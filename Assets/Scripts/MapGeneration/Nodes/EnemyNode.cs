using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNode : MapNode
{
    public EnemyNode(float x, float y)
    {
        position = new Vector2(x, y);
        nodeType = NodeType.ENEMY_TYPE;
        DrawNode(x, y);
    }

    public override void SelectNode()
    {
        base.SelectNode();
    }

    public override void DrawNode(float x, float y)
    {
        MusicNote note = new MusicNote(x, y);
    }
}
