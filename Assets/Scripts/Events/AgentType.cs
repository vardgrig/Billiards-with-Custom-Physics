using System;
using System.Collections.Generic;

public static class AgentType
{
    public static Dictionary<Type, AgentTypeEnum> ClassToType = new Dictionary<Type, AgentTypeEnum>
    {
        //{ typeof(NullObject), AgentType.NULL },
        { typeof(Cue), AgentTypeEnum.CUE },
        { typeof(Ball), AgentTypeEnum.BALL },
        { typeof(Pocket), AgentTypeEnum.POCKET },
        { typeof(LinearCushionSegment), AgentTypeEnum.LINEAR_CUSHION_SEGMENT },
        { typeof(CircularCushionSegment), AgentTypeEnum.CIRCULAR_CUSHION_SEGMENT }
    };
    public static Dictionary<AgentTypeEnum, Type> TypeToClass = new Dictionary<AgentTypeEnum, Type>
    {
        //{ AgentTypeEnum.NULL, typeof(NullObject) },
        { AgentTypeEnum.CUE, typeof(Cue) },
        { AgentTypeEnum.BALL, typeof(Ball) },
        { AgentTypeEnum.POCKET, typeof(Pocket) },
        { AgentTypeEnum.LINEAR_CUSHION_SEGMENT, typeof(LinearCushionSegment) },
        { AgentTypeEnum.CIRCULAR_CUSHION_SEGMENT, typeof(CircularCushionSegment) }
    };
}
