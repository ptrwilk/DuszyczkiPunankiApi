using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace DuszyczkiPunankiApi.Hubs;

public interface IMessageHub
{
    Task Refresh(string message);
}

public class MessageHub : Hub<IMessageHub>
{
    public static readonly ConcurrentDictionary<string, Guid> Dictionary = new();

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Dictionary.TryRemove(Context.ConnectionId, out _);
        
        return base.OnDisconnectedAsync(exception);
    }

    public async Task Join(Guid orderId)
    {
        if (Dictionary.ContainsKey(Context.ConnectionId))
        {
            Dictionary.TryRemove(Context.ConnectionId, out _);
        }

        Dictionary.TryAdd(Context.ConnectionId, orderId);

        await Task.CompletedTask;
    }
}