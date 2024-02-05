namespace HostedServiceExtension.Example.Handlers;

using System;
using System.Buffers;

using HostedServiceExtension.Example.Handlers.Commands;

using Microsoft.AspNetCore.Connections;

using Smart.Threading;

public sealed class SampleHandler : ConnectionHandler
{
    private enum CommandResult
    {
        Success,
        Unknown,
        Quit
    }

    private readonly ILogger<SampleHandler> log;

    private readonly ICommand[] commands;

    public SampleHandler(ILogger<SampleHandler> log, IEnumerable<ICommand> commands)
    {
        this.log = log;
        this.commands = commands.ToArray();
    }

    public override async Task OnConnectedAsync(ConnectionContext connection)
    {
        log.DebugHandlerConnected(connection.ConnectionId);

        try
        {
            using var timeout = new ReusableCancellationTokenSource();
            while (true)
            {
                timeout.CancelAfter(30_000);
                var result = await connection.Transport.Input.ReadAsync(timeout.Token);
                var buffer = result.Buffer;

                var running = true;
                while (!buffer.IsEmpty && ReadLine(ref buffer, out var line))
                {
                    var commandResult = await ProcessLineAsync(line, connection.Transport.Output);
                    if (commandResult == CommandResult.Unknown)
                    {
                        connection.Transport.Output.WriteAndAdvanceNg();
                    }
                    else if (commandResult == CommandResult.Quit)
                    {
                        running = false;
                        break;
                    }

                    await connection.Transport.Output.FlushAsync(CancellationToken.None);
                }

                if (!running || result.IsCompleted)
                {
                    break;
                }

                connection.Transport.Input.AdvanceTo(buffer.Start, buffer.End);

                timeout.Reset();
            }
        }
        catch (OperationCanceledException)
        {
            // Ignore
        }
        finally
        {
            log.DebugHandlerDisconnected(connection.ConnectionId);
        }
    }

    private static bool ReadLine(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> line)
    {
        var reader = new SequenceReader<byte>(buffer);
        if (reader.TryReadTo(out ReadOnlySequence<byte> l, "\r\n"u8))
        {
            buffer = buffer.Slice(reader.Position);
            line = l;
            return true;
        }

        line = default;
        return false;
    }

    private async ValueTask<CommandResult> ProcessLineAsync(ReadOnlySequence<byte> buffer, IBufferWriter<byte> writer)
    {
        MemoryHelper.Split(ref buffer, out var first, (byte)' ');
        foreach (var command in commands)
        {
            if (command.Match(first))
            {
                return await command.ExecuteAsync(buffer, writer) ? CommandResult.Success : CommandResult.Quit;
            }
        }

        return CommandResult.Unknown;
    }
}
