namespace HostedServiceExtension.Example.Handlers;

using Microsoft.AspNetCore.Connections;

#pragma warning disable CA1848
public sealed class SampleHandler : ConnectionHandler
{
    private readonly ILogger<SampleHandler> logger;

    public SampleHandler(ILogger<SampleHandler> logger)
    {
        this.logger = logger;
    }

    // TODO
    public override async Task OnConnectedAsync(ConnectionContext connection)
    {
        logger.LogInformation(connection.ConnectionId + " connected");

        while (true)
        {
            var result = await connection.Transport.Input.ReadAsync();
            var buffer = result.Buffer;

            foreach (var segment in buffer)
            {
                await connection.Transport.Output.WriteAsync(segment);
            }

            if (result.IsCompleted)
            {
                break;
            }

            connection.Transport.Input.AdvanceTo(buffer.End);
        }

        logger.LogInformation(connection.ConnectionId + " disconnected");
    }
}
#pragma warning restore CA1848
