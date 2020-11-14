using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    public enum MinerStates
    {
        Idle,
        Patrol,
        Mining,
        Returning,
        AllStates
    }

    public int maxX;
    public int maxZ;
    public float posY;
    private bool goToHQ;
    public MinerStates currentState;
    public float timer;
    public float speed;
    Vector3[] path;
    public int index;
    public LayerMask groundMask;
    public MinerSight sight;
    public Vector3 spotPos;

    private float maxTimeIdle = 5f;
    public bool reachedPathEnd;
    public bool goToSpot;

    public Animator anim;

    public float goldMined;
    public float goldExtractionRate;
    private float maxGoldCarried = 100.0f;
    private void Start()
    {
        reachedPathEnd = true;
        goToSpot = true;
    }
    private void Update()
    {
        switch (currentState)
        {
            case MinerStates.Idle:
                timer += Time.deltaTime;
                if (timer >= maxTimeIdle)
                {
                    currentState = MinerStates.Patrol;
                    timer = 0;
                }
                break;
            case MinerStates.Patrol:
                if (reachedPathEnd)
                {
                    index = 0;
                    reachedPathEnd = false;
                    PathRequestManager.RequestPath(transform.position, GetPos(), OnPathFound);
                }
                break;
            case MinerStates.Mining:
                if (goToSpot)
                {
                    goToSpot = false;
                    reachedPathEnd = false;
                    index = 0;
                    PathRequestManager.RequestPath(transform.position, spotPos, OnPathFound);
                }
                if (reachedPathEnd)
                {
                    if (goldMined<maxGoldCarried)
                    {
                        goldMined += Time.deltaTime * goldExtractionRate;
                    }
                    else
                    {
                        goToHQ = true;
                        currentState = MinerStates.Returning;
                    }
                }
                break;
            case MinerStates.Returning:
                if (goToHQ)
                {
                    goToHQ = false;
                    index = 0;
                    reachedPathEnd = false;
                    PathRequestManager.RequestPath(transform.position, GameManager.Get().spawnPoint.position, OnPathFound);
                }
                if (reachedPathEnd)
                {
                    GameManager.Get().gold += (int)goldMined;
                    goldMined = 0;
                    currentState = MinerStates.Patrol;
                }
                break;
            case MinerStates.AllStates:
                break;
            default:
                break;
        }
    }

    public void OnPathFound(Vector3[] newPath,bool pathSuccess)
    {
        if (pathSuccess)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
            anim.SetBool("Walk_Anim", true);
            anim.SetBool("Idle", false);
        }
        else
        {
            reachedPathEnd = false;
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
                if (index>=path.Length)
                {
                    reachedPathEnd = true;
                    anim.SetBool("Walk_Anim", false);
                    anim.SetBool("Idle", true);
                    yield break;
                }
                currentWaypoint = path[index];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint,speed*Time.deltaTime);
            transform.LookAt(currentWaypoint);
            yield return null;
        }
    }

    public Vector3 GetPos()
    {
        Vector3 pos = new Vector3(Random.Range(-maxX, maxX), posY, Random.Range(-maxZ, maxZ));

        return pos;
    }

    public void OnDrawGizmos()
    {
        if (path!=null)
        {
            for (int i = index; i < path.Length; i++)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i==index)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i-1], path[i]);
                }
            }
        }
    }
}
