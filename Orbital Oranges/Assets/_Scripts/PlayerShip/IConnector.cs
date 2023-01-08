public interface IConnector {
    public void HandleDisconnect(Dir dir, int slot, IConnectable connectable);
}