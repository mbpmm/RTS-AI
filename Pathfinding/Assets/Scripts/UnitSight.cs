using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitSight : MonoBehaviour
{
    public float fovAngle;
    public bool mineInSight;
    public LayerMask mask;

    public SphereCollider detectionCol;
    public float radius;
    // Start is called before the first frame update
    void Awake()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Mine")
        {
            mineInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fovAngle * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, direction.normalized, out hit, detectionCol.radius, mask, QueryTriggerInteraction.Ignore))
                {
                    if (hit.transform.gameObject.tag == "Mine")
                    {
                        mineInSight = true;
                    }
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Mine")
        {
            mineInSight = false;
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        angleInDegrees += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
