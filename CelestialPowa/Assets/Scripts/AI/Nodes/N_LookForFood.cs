using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_LookForFood : S_Node
{
    private S_HumanAI owner;
    private Transform foodTarget;
    // Maybe need navemesh agent to work
    
    public N_LookForFood(S_HumanAI _owner, Transform _foodTarget)
    {
        this.owner = _owner;
        this.foodTarget = _foodTarget;
    }

    public override NodeState Evaluate()
    { 
        float distance = Vector3.Distance(foodTarget.position, owner.transform.position);

        if (distance > 0.2)
        {
            // Logic to move to foodTarget (transform) and apply GoTo method
            
            //owner.GoTo(foodTarget.position);
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
