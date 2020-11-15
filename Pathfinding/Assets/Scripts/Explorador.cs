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

    public int maxX;
    public int maxZ;
    public float posY;
    public ExploradorStates currentState;
    public float timer;
    public float speed;
    Vector3[] path;
    public int index;
    public LayerMask groundMask;
    public ExplorerSight sight;
    public Vector3 spotPos;

    private float maxTimeIdle = 5f;
    public bool reachedPathEnd;
    public bool goToSpot;

    // Start is called before the first frame update
    void Start()
    {
        reachedPathEnd = true;
        goToSpot = true;
    }

    // Update is called once per frame
    void Update()
    {
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
                if (reachedPathEnd)
                {
                    index = 0;
                    reachedPathEnd = false;
                    PathRequestManager.RequestPath(transform.position, GetRandomPos(), OnPathFound);
                }
                break;
            case ExploradorStates.Marking:
                if (goToSpot)
                {
                    goToSpot = false;
                    reachedPathEnd = false;
                    index = 0;
                    PathRequestManager.RequestPath(transform.position, spotPos, OnPathFound);
                }
                if (reachedPathEnd)
                {
                    currentState = ExploradorStates.Patrol;
                }
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
        else
        {
            reachedPathEnd = true;
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
                    reachedPathEnd = true;
                    yield break;
                }
                currentWaypoint = path[index];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            transform.LookAt(currentWaypoint);
            yield return null;
        }
    }

    public Vector3 GetRandomPos()
    {
        // agarrar punto del mouse 
        //RaycastHit hitPos;

        //Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitPos, 1000, groundMask);

        //return hitPos.point;

        //posicion random en una zona
        Vector3 pos = new Vector3(Random.Range(-maxX, maxX), posY, Random.Range(-maxZ, maxZ));

        return pos;
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
