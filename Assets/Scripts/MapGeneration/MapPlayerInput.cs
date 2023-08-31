using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapPlayerInput : MonoBehaviour
{
    public MapGenerator mapGenerator;

    MapSongPlayer mapSongPlayer;

    MapMenu mapMenu;

    int neighbourIdx = 0;
    bool canChangeNeighbour = true;

    void Start()
    {
        mapSongPlayer = GameObject.Find("MapSongPlayer").GetComponent<MapSongPlayer>();
        mapMenu = GameObject.Find("MapUI").GetComponent<MapMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        List<PathNode> neighbours = GetCurrentNodeNeihgbours();

        if (neighbours.Count <= 0) return;

        if (canChangeNeighbour && Input.GetAxis("Horizontal") <= -0.85f)
        {
            if (neighbourIdx >= neighbours.Count) neighbourIdx = neighbours.Count - 1;

            neighbourIdx--;

            if (neighbourIdx < 0) neighbourIdx = 0;

            MoveCurrentNode(neighbours[neighbourIdx]);
            StartCoroutine(CanChangeNeighbour());
        }

        if (canChangeNeighbour && Input.GetAxis("Horizontal") >= 0.85f)
        {
            if (neighbourIdx < 0) neighbourIdx = 0;

            neighbourIdx++;
            
            if (neighbourIdx >= neighbours.Count) neighbourIdx = (neighbours.Count - 1);

            MoveCurrentNode(neighbours[neighbourIdx]);
            StartCoroutine(CanChangeNeighbour());
        }

        if (Input.GetButtonDown("A"))
        {
            SelectNode(neighbours[neighbourIdx]);
        }
    }

    IEnumerator CanChangeNeighbour()
    {
        canChangeNeighbour = false;

        yield return new WaitForSeconds(0.25f);

        canChangeNeighbour = true;
    }

    List<PathNode> GetCurrentNodeNeihgbours()
    {
        List<PathNode> neighbours = mapGenerator.GetPathNodeNeighbours(mapGenerator.currentNode);

        if (neighbours.Count <= 1) return neighbours;

        List<PathNode> neighboursDistinct = neighbours;
        for (int i = 0; i < neighbours.Count - 1; i++)
        {
            if (neighbours[i].x == neighbours[i+1].x && neighbours[i].y == neighbours[i + 1].y)
            {
                neighboursDistinct.Remove(neighboursDistinct[i]);
            }
        }

        neighboursDistinct = neighboursDistinct.OrderBy(node => node.x).ToList();

        return neighboursDistinct;
    }

    void MoveCurrentNode(PathNode nextNode)
    {
        mapGenerator.playerSelectorCylinder.transform.position = new Vector3(nextNode.x, -0.7f, nextNode.y);
        mapSongPlayer.ChangeAudioClip(nextNode);
        mapMenu.ChangeNodeUI(nextNode);
    }

    void SelectNode(PathNode nextNode)
    {
        mapGenerator.SelectNode(nextNode);
    }
}
