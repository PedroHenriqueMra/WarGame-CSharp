using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

public class SendOutput
{
    public Task sendAsync(Session session, OutputEnvelope envelope)
    {
        string json = JsonSerializer.Serialize(envelope);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);

        return session.SendAsync(jsonBytes);
    }
}
