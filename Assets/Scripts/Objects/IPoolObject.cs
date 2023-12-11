public interface IPoolObject
{
    public IPoolObject Copy(bool dropHistory = false);
    string id { get; set; }
}
