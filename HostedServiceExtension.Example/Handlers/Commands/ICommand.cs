namespace HostedServiceExtension.Example.Handlers.Commands;

using System.Buffers;

public interface ICommand
{
    bool Match(ReadOnlySequence<byte> command);

    ValueTask<bool> ExecuteAsync(ReadOnlySequence<byte> options, IBufferWriter<byte> writer);
}
