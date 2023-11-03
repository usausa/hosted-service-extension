namespace HostedServiceExtension.Example.Handlers.Commands;

using System.Buffers;

using HostedServiceExtension.Example.Service;

public sealed class GetCommand : ICommand
{
    private readonly FeatureService featureService;

    public GetCommand(FeatureService featureService)
    {
        this.featureService = featureService;
    }

    public bool Match(ReadOnlySpan<byte> command) => command.SequenceEqual("get"u8);

    public ValueTask<bool> ExecuteAsync(ReadOnlyMemory<byte> options, IBufferWriter<byte> writer)
    {
        var value = featureService.QueryFeature();
        writer.WriteAndAdvanceOk(value ? "on"u8 : "off"u8);

        return ValueTask.FromResult(true);
    }
}
