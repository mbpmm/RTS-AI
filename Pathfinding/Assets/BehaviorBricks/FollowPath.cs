using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using BBUnity.Actions;


    [Action("MyActions/FollowPath")]
[Help("Sigue el camino buscado ")]
public class FollowPath : GOAction
{

    [InParam("aldeano")]
    private Aldeano aldeano;

    
    // Main class method, invoked by the execution engine.
    public override TaskStatus OnUpdate()
    {
        aldeano = gameObject.GetComponent<Aldeano>();
        // Instantiate the bullet prefab.
        aldeano.index = 0;

        PathRequestManager.RequestPath(aldeano.gameObject.transform.position, aldeano.GetPos(), aldeano.OnPathFound);

        return TaskStatus.COMPLETED;

    } // OnUpdate

} // class ShootOnce