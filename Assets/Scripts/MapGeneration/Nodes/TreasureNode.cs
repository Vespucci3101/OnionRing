using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureNode : MapNode
{
    public TreasureNode(float x, float y)
    {
        position = new Vector2(x, y);
        nodeType = NodeType.TREASURE_TYPE;

        DrawNode(x, y);
    }

    public override void SelectNode()
    {
        base.SelectNode();
    }

    public override void DrawNode(float x, float y)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(x, 0, y);
        sphere.GetComponent<Renderer>().material.color = Color.yellow;

        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = new Vector3(x + 0.3f, 0.75f, y);
        cylinder.transform.localScale = new Vector3(0.35f, 0.75f, 0.35f);
        cylinder.GetComponent<Renderer>().material.color = Color.yellow;
    }
}
