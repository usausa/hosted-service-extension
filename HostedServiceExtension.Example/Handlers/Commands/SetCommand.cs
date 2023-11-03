namespace HostedServiceExtension.Example.Handlers.Commands;

using System.Buffers;

using HostedServiceExtension.Example.Service;

public sealed class SetCommand : ICommand
{
    private readonly FeatureService featureService;

    public SetCommand(FeatureService featureService)
    {
        this.featureService = featureService;
    }

    public bool Match(ReadOnlySpan<byte> command) => command.SequenceEqual("set"u8);

    public ValueTask<bool> ExecuteAsync(ReadOnlyMemory<byte> options, IBufferWriter<byte> writer)
    {
        if ("1"u8.SequenceEqual(options.Span))
        {
            featureService.UpdateFeature(true);
            writer.WriteAndAdvanceOk();
        }
        else if ("0"u8.SequenceEqual(options.Span))
        {
            featureService.UpdateFeature(false);
            writer.WriteAndAdvanceOk();
        }
        else
        {
            writer.WriteAndAdvanceNg();
        }

        return ValueTask.FromResult(true);
    }
}
