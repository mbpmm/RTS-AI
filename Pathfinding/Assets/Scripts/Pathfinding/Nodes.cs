using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : IHeapItem<Nodes>
{
    public Vector3 pos;
    public int G_cost;
    public int H_cost;
    public int gridX;
    public int gridY;
    public List<Nodes> neighbords;
    public bool is_Walkable;
    public Nodes parent;
    int heapIndex;

    public Nodes(bool isWalkable,Vector3 position, int _gridX, int _gridY)
    {
        is_Walkable = isWalkable;
        pos = position;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int F_cost
    {
        get { return G_cost + H_cost; }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Nodes nodeToCompare)
    {
        int compare = F_cost.CompareTo(nodeToCompare.F_cost);

        if (compare == 0)
        {
            compare = H_cost.CompareTo(nodeToCompare.H_cost);
        }
        return -compare;
    }
}
