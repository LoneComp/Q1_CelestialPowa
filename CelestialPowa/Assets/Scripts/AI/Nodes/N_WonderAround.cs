using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_WonderAround : S_Node
{
    private S_HumanAI owner;
    private Transform target;
    // Maybe need navemesh agent to work
    
    public N_WonderAround(S_HumanAI _owner, Transform _target)
    {
        this.owner = _owner;
        this.target = _target;
    }

    public override NodeState Evaluate()
    { 
        float distance = Vector3.Distance(target.position, owner.transform.position);

        if (distance > 0.2)
        {
            // Logic to move to target (transform) and apply GoTo method
            
            //owner.GoTo(target.position);
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
