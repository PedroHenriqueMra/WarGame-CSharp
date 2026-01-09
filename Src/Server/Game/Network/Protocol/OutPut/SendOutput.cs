using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

public class SendOutput
{
    public Task SendAsync<TPayload>(Session session, OutputEnvelope<TPayload> envelope)
    where TPayload : IPayload
    {
        string json = JsonSerializer.Serialize(envelope);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);

        return session.SendAsync(jsonBytes);
    }
}
