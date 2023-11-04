namespace HostedServiceExtension.Example.Handlers;

using HostedServiceExtension.Example.Handlers.Commands;

using Microsoft.AspNetCore.Connections;

#pragma warning disable CA1848
public sealed class SampleHandler : ConnectionHandler
{
    private readonly ILogger<SampleHandler> logger;

    private readonly ICommand[] commands;

    public SampleHandler(ILogger<SampleHandler> logger, IEnumerable<ICommand> commands)
    {
        this.logger = logger;
        this.commands = commands.ToArray();
    }

    public override async Task OnConnectedAsync(ConnectionContext connection)
    {
        logger.LogInformation("Handler connected. connectionId=[{ConnectionId}]", connection.ConnectionId);

        while (true)
        {
            // TODO command dispatcher
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

        logger.LogInformation("Handler disconnected. connectionId=[{ConnectionId}]", connection.ConnectionId);
    }
}
#pragma warning restore CA1848
