using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


// Source : https://www.youtube.com/watch?v=alU04hvz6L4

public class Pathfinding
{
    private List<PathNode> openList;
    private List<PathNode> closedList;

    public Pathfinding() {}

    private List<PathNode> TransformPointsToPathnodes(List<Vector2> points)
    {
        List<PathNode> pathNodes = new List<PathNode>();
        for (int i = 0; i < points.Count; i++)
        {
            pathNodes.Add(new PathNode(points[i].x, points[i].y));
        }

        return pathNodes;
    }

    public List<PathNode> FindPath(Vector2 start, Vector2 end, List<Vector2> points, List<Triangle> triangles)
    {
        List<PathNode> pathNodes = TransformPointsToPathnodes(points);
        PathNode startNode = new PathNode(start.x, start.y);
        PathNode endNode = new PathNode(end.x, end.y);

        pathNodes.Add(startNode);
        pathNodes.Add(endNode);

        openList = new List<PathNode>() { startNode };
        closedList = new List<PathNode>();

        for (int i = 0; i < pathNodes.Count; i++)
        {
            pathNodes[i].gCost = int.MaxValue;
            pathNodes[i].CalculateFCost();
            pathNodes[i].previousNode = null;
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            
            if (currentNode.x == endNode.x && currentNode.y == endNode.y)
            {
                // reach end
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode, triangles, pathNodes))
            {
                if (closedList.Contains(neighbourNode))
                {
                    continue;
                }

                float tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.previousNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        return null;
    }

    private List<PathNode> GetNeighbourList(PathNode currentnode, List<Triangle> triangles, List<PathNode> pathNodes)
    {
        List<PathNode> neighbourList = new List<PathNode>();
        List<Vector3> neighbourListVector3 = new List<Vector3>();

        Vector3 currentPos = new Vector3(currentnode.x, 0, currentnode.y);
        for (int i = 0; i < triangles.Count; i++)
        {
            if (triangles[i].v1.position == currentPos)
            {
                neighbourListVector3.Add(triangles[i].v2.position);
                neighbourListVector3.Add(triangles[i].v3.position);
            }
            else if (triangles[i].v2.position == currentPos)
            {
                neighbourListVector3.Add(triangles[i].v1.position);
                neighbourListVector3.Add(triangles[i].v3.position);
            }
            else if (triangles[i].v3.position == currentPos)
            {
                neighbourListVector3.Add(triangles[i].v2.position);
                neighbourListVector3.Add(triangles[i].v1.position);
            }
        }

        neighbourListVector3 = neighbourListVector3.Distinct().ToList();

        for (int i = 0; i < neighbourListVector3.Count; i++)
        {
            for (int j = 0; j < pathNodes.Count; j++)
            {
                if (pathNodes[j].x == neighbourListVector3[i].x && pathNodes[j].y == neighbourListVector3[i].z)
                {
                    neighbourList.Add(pathNodes[j]);
                }
            }
        }

        return neighbourList;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();

        path.Add(endNode);

        PathNode currentnode = endNode;

        while (currentnode.previousNode != null)
        {
            path.Add(currentnode.previousNode);
            currentnode = currentnode.previousNode;
        }

        path.Reverse();
        return path;
    }

    private float CalculateDistanceCost(PathNode a, PathNode b)
    {
        float distX = Mathf.Abs(a.x - b.x);
        float distY = Mathf.Abs(a.y - b.y);
        return Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for (int i = 0; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
