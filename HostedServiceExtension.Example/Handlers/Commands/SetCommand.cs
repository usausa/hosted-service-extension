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

    public bool Match(ReadOnlySequence<byte> command) => command.SequentialEqual("set"u8);

    public ValueTask<bool> ExecuteAsync(ReadOnlySequence<byte> options, IBufferWriter<byte> writer)
    {
        if (options.SequentialEqual("1"u8))
        {
            featureService.UpdateFeature(true);
            writer.WriteAndAdvanceOk();
        }
        else if (options.SequentialEqual("0"u8))
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
