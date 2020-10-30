using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorador : MonoBehaviour
{
    public enum ExploradorStates
    {
        Idle,
        Patrol,
        Marking,
        AllStates
    }

    public ExploradorStates currentState;
    public float timer;
    public float speed;
    Vector3[] path;
    public int index;
    public LayerMask groundMask;


    private float maxTimeIdle = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            index = 0;
            PathRequestManager.RequestPath(transform.position, GetPos(), OnPathFound);
        }

        switch (currentState)
        {
            case ExploradorStates.Idle:
                timer += Time.deltaTime;
                if (timer>=maxTimeIdle)
                {
                    currentState = ExploradorStates.Patrol;
                    timer = 0;
                }
                break;
            case ExploradorStates.Patrol:
                break;
            case ExploradorStates.Marking:
                break;
            case ExploradorStates.AllStates:
                break;
            default:
                break;
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccess)
    {
        if (pathSuccess)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                index++;
                if (index >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[index];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            transform.LookAt(currentWaypoint);
            yield return null;
        }
    }

    public Vector3 GetPos()
    {
        RaycastHit hitPos;

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitPos, 1000, groundMask);

        return hitPos.point;
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = index; i < path.Length; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == index)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}
