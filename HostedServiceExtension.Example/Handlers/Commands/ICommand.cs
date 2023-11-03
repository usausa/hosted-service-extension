namespace HostedServiceExtension.Example.Handlers.Commands;

using System.Buffers;

public interface ICommand
{
    bool Match(ReadOnlySpan<byte> command);

    ValueTask<bool> ExecuteAsync(ReadOnlyMemory<byte> options, IBufferWriter<byte> writer);
}
