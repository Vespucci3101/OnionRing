using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public float radius = 1;
    public Vector2 regionSize = Vector2.one;
    public int rejectionSamples = 30;
    public float displayRadius = 1;

    public PathNode currentNode;
    public GameObject playerSelectorCylinder;

    Map map;

    // Start is called before the first frame update
    void Start()
    {
        map = Map.GetMapInstance();

        if (map == null) return;

        if (map.isMapGenerated) map.LoadMap();
        else map.GenerateMap();

        playerSelectorCylinder = map.playerSelectorCylinder;
        currentNode = map.currentNode;
    }

    public List<PathNode> GetPathNodeNeighbours(PathNode currentNode)
    {
        if (map == null) return null;
        return map.GetPathNodeNeighbours(currentNode);
    }

    public void SelectNode(PathNode nextNode)
    {
        if (map == null) return;
        map.SelectNode(nextNode);
        currentNode = map.currentNode;
    }
}
