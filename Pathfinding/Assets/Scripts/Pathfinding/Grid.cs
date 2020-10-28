using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool displayGrid;
    public int x_count;
    public int y_count;
    public List<Nodes> nodes;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Nodes[,] grid;
    public LayerMask obstacleMask;
    public float nodeDiameter;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        x_count = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        y_count = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    public void CreateGrid()
    {
        grid = new Nodes[x_count, y_count];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int i = 0; i < x_count; i++)
        {
            for (int j = 0; j < y_count; j++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (i * nodeDiameter + nodeRadius) + Vector3.forward * (j * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, obstacleMask));
                grid[i, j] = new Nodes(walkable, worldPoint, i, j);
            }
        }
    }

    public List<Nodes> GetNeighbours(Nodes node)
    {
        List<Nodes> neighbours = new List<Nodes>();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                int checkX = node.gridX + i;
                int checkY = node.gridY + j;

                if (checkX >= 0 && checkX < x_count && checkY >= 0 && checkY < y_count)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Nodes NodeFromWorldPoint(Vector3 worldPos)
    {
        float percentX = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPos.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((x_count - 1) * percentX);
        int y = Mathf.RoundToInt((y_count - 1) * percentY);

        return grid[x, y];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null && displayGrid)
        {
            foreach (Nodes n in grid)
            {
                Gizmos.color = (n.is_Walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.pos, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }

    public int MaxSize
    {
        get
        {
            return x_count*y_count;
        }
    }
}
