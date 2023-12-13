public interface IAgent<T> where T : IPoolObject
{
    string id { get; set; }
    AgentTypeEnum agentType { get; set; }
    T initial { get; set; }
    T final { get; set; }

    void SetInitial(T obj);
}
