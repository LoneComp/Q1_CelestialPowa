using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Hunger : S_Node
{
    private S_HumanAI owner;
    private float threshold;
    
    public N_Hunger(S_HumanAI _owner, float _threshold)
    {
        this.owner = _owner;
        this.threshold = _threshold;
    }
    
    public override NodeState Evaluate()
    {
        return owner.currentHunger <= threshold ? NodeState.SUCCESS : NodeState.FAILURE;
    }
    
}
