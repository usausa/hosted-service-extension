namespace HostedServiceExtension.Example.Handlers.Commands;

using System.Buffers;

public sealed class ExitCommand : ICommand
{
    public bool Match(ReadOnlySequence<byte> command) => command.SequentialEqual("exit"u8);

    public ValueTask<bool> ExecuteAsync(ReadOnlySequence<byte> options, IBufferWriter<byte> writer) => ValueTask.FromResult(false);
}
