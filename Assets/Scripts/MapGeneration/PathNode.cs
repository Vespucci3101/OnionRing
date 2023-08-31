using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Source : https://www.youtube.com/watch?v=alU04hvz6L4

public class PathNode
{
    public float x;
    public float y;

    public float gCost;
    public float hCost;
    public float fCost;

    public PathNode previousNode;

    public PathNode(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public void CalculateFCost()
    {
        fCost = hCost + gCost;
    }
}
