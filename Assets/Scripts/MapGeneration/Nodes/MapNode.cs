using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode
{
    public Vector2 position;
    public int nodeType;

    public MapNode() 
    {
        nodeType = NodeType.NO_TYPE;
    }

    virtual public void SelectNode() { }
    virtual public void DrawNode(float x, float y) { }
}
