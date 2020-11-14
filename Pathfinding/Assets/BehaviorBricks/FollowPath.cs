using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using BBUnity.Actions;


[Action("MyActions/FollowPath")]
[Help("Sigue el camino buscado ")]
public class FollowPath : GOAction
{

    [InParam("Miner")]
    private Miner miner;

    
    // Main class method, invoked by the execution engine.
    public override TaskStatus OnUpdate()
    {
        miner = gameObject.GetComponent<Miner>();
        // Instantiate the bullet prefab.
        if (miner.reachedPathEnd)
        {
            miner.index = 0;
            miner.reachedPathEnd = false;
            PathRequestManager.RequestPath(miner.gameObject.transform.position, miner.GetPos(), miner.OnPathFound);
        }
        

        return TaskStatus.COMPLETED;

    } // OnUpdate

} // class ShootOnce