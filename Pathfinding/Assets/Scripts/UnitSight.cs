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
    public Explorador explorador;
    // Start is called before the first frame update
    void Awake()
    {
        explorador = GetComponent<Explorador>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Flag")
        {
            mineInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fovAngle * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, direction.normalized, out hit, detectionCol.radius+1, mask))
                {
                    if (hit.transform.gameObject.tag == "Flag" && explorador.currentState!=Explorador.ExploradorStates.Marking)
                    {
                        mineInSight = true;
                        explorador.currentState = Explorador.ExploradorStates.Marking;
                        explorador.spotPos = hit.transform.position;
                        explorador.goToSpot = true;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Flag")
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
