using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using BBUnity.Conditions;


[Condition("MyActions/GoToSpot")]
[Help("COndicion si puede ir a otro punto o no ")]
public class GoToSpot : GOCondition
{
    [InParam("goToSpot")]
    public bool goToSpot;

    [InParam("Miner")]
    private Miner miner;


    // Main class method, invoked by the execution engine.
    public override bool Check()
    {
        Miner miner = gameObject.GetComponent<Miner>();

        if (goToSpot)
        {
            if (miner != null)
            {
                if (miner.reachedPathEnd)
                    return true;
            }

            return false;
        }
        else
        {
            if (miner != null)
            {
                if (!miner.reachedPathEnd)
                    return false;
            }
            return false;
        }
    } // OnUpdate

} // class ShootOnce