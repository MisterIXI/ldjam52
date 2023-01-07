public interface IConnectable {
    public bool IsConnected { get; }
    public void Connect(Dir dir, int slot, IConnector connector);
    public void Disconnect();
}