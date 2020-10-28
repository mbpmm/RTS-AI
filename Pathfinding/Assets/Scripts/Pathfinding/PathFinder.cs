using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class PathFinder : MonoBehaviour
{
    PathRequestManager requestMan;
    Grid grid;

    private void Awake()
    {
        requestMan = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
    }
    public void StartFindPath(Vector3 startPos,Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }
    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Nodes startNode = grid.NodeFromWorldPoint(startPos);
        Nodes targetNode = grid.NodeFromWorldPoint(targetPos);

        if (startNode.is_Walkable && targetNode.is_Walkable)
        {
            Heap<Nodes> openSet = new Heap<Nodes>(grid.MaxSize);
            HashSet<Nodes> closedSet = new HashSet<Nodes>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Nodes currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    sw.Stop();
                    print("Path found: " + sw.ElapsedMilliseconds);
                    pathSuccess = true;
                    break;
                }

                foreach (Nodes neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.is_Walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovCostToNeighbour = currentNode.G_cost + GetDistance(currentNode, neighbour);

                    if (newMovCostToNeighbour < neighbour.G_cost || !openSet.Contains(neighbour))
                    {
                        neighbour.G_cost = newMovCostToNeighbour;
                        neighbour.H_cost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                        else
                        {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }


            }
        }
        yield return null;
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode,targetNode);
        }
        requestMan.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector3[] RetracePath(Nodes startNode, Nodes endNode)
    {
        List<Nodes> path = new List<Nodes>();
        Nodes currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<Nodes> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 dirOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 dirNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (dirNew != dirOld)
            {
                waypoints.Add(path[i].pos);
            }
            dirOld = dirNew;
        }
        return waypoints.ToArray();
    }

    int GetDistance(Nodes a, Nodes b)
    {
        int distX = Mathf.Abs(a.gridX - b.gridX);
        int distY = Mathf.Abs(a.gridY - b.gridY);

        if (distX>distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }

        return 14 * distX + 10 * (distY - distX);
    }
}
