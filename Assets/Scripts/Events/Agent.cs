public class Agent : IAgent<IPoolObject>
{
    public string id { get; set; }
    public AgentTypeEnum agentType { get; set; }
    public IPoolObject initial { get; set; }
    public IPoolObject final { get; set; }

    public void SetInitial(IPoolObject _object)
    {
        if (agentType == AgentTypeEnum.NULL)
            return;

        if(agentType == AgentTypeEnum.BALL)
        {
            initial = _object.Copy(false);
        }
        else
        {
            initial = _object.Copy();
        }
    }   
    public void SetFinal(IPoolObject _object)
    {
        if (agentType == AgentTypeEnum.NULL)
            return;

        if(agentType == AgentTypeEnum.BALL)
        {
            final = _object.Copy(false);
        }
        else
        {
            final = _object.Copy();
        }
    }
    public bool Matches(IPoolObject _object)
    {
        var correct_class = AgentType.ClassToType[_object.GetType()] == agentType;
        return correct_class && _object.id == id;
    }
    public static Agent FromObject(IPoolObject _object, bool setInitial = false)
    {
        var agent = new Agent()
        {
            id = _object.id,
            agentType = AgentType.ClassToType[_object.GetType()]
        };
        if (setInitial)
            agent.SetInitial(_object);
        return agent;
    }
    public Agent Copy()
    {
        return null; // InitEvolve(this);
    }
}