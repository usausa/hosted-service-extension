namespace HostedServiceExtension.Example.Handlers.Commands;

using System.Buffers;

public sealed class ExitCommand : ICommand
{
    public bool Match(ReadOnlySpan<byte> command) => command.SequenceEqual("exit"u8);

    public ValueTask<bool> ExecuteAsync(ReadOnlyMemory<byte> options, IBufferWriter<byte> writer) => ValueTask.FromResult(false);
}
