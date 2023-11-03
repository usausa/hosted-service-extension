namespace HostedServiceExtension.Example.Service;

public sealed class FeatureService
{
    private readonly object sync = new();

    private bool enable;

    public void UpdateFeature(bool value)
    {
        lock (sync)
        {
            enable = value;
        }
    }

    public bool QueryFeature()
    {
        lock (sync)
        {
            return enable;
        }
    }
}
