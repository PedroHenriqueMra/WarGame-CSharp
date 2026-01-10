public interface IConnectSession<TProtocol>
{
    public bool Connect(HttpContext context,TProtocol socket, ITransportSender transport, out Session? session); 
}
