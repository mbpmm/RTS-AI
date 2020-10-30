using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Aldeano : MonoBehaviour
{
    public Transform target;
    public float speed;
    Vector3[] path;
    public int index;
    public LayerMask groundMask;

    public Animator anim;

    private void Start()
    {
        //PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    index = 0;
        //    PathRequestManager.RequestPath(transform.position, GetPos(), OnPathFound);
        //}
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
        RaycastHit hitPos;

        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitPos, 1000, groundMask);

        return hitPos.point;
    }

    public void OnDrawGizmos()
    {
        if (path!=null)
        {
            for (int i = index; i < path.Length; i++)
            {
                Gizmos.color = Color.green;
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
