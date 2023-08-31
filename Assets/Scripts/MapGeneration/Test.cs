using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	public float radius = 1;
	public Vector2 regionSize = Vector2.one;
	public int rejectionSamples = 30;
	public float displayRadius = 1;

	List<Vector2> points;
	List<Vector3> sites;
	List<Vector3> points3D;

	List<Triangle> triangles;

    Pathfinding path;
    List<PathNode> pathNodesFirstAStar;
    List<PathNode> pathNodesSecondAStar;
    List<PathNode> pathNodesThirdtAStar;

	void OnValidate()
	{
		triangles = new List<Triangle>();
		sites = new List<Vector3>();
        points3D = new List<Vector3>();
		points = PoissonDiskSampling.GeneratePoints(radius, regionSize, rejectionSamples);
        Vector2 startPoint = new Vector2(regionSize.x / 2, regionSize.y * 0.05f);
        Vector2 endPoint = new Vector2(regionSize.x / 2, regionSize.y * 0.95f);

        points.Add(startPoint);
        points.Add(endPoint);

		for (int i = 0; i < points.Count; i++)
		{
			Vector3 site = new Vector3(points[i].x, 0, points[i].y);
			sites.Add(site);
			points3D.Add(site);
		}

		triangles = DelaunayTriangulation.TriangulateByFlippingEdges(points3D);

        path = new Pathfinding();
        pathNodesFirstAStar = new List<PathNode>();
        pathNodesSecondAStar = new List<PathNode>();
        pathNodesThirdtAStar = new List<PathNode>();

        pathNodesFirstAStar = path.FindPath(startPoint, endPoint, points, triangles);

        for (int i = 0; i < points.Count; i++)
        {
            // Changer le j += 3 pour modifier les points a enlever
            for (int j = 0; j < pathNodesFirstAStar.Count; j += 3)
            {
                if (points[i].x == pathNodesFirstAStar[j].x && points[i].y == pathNodesFirstAStar[j].y)
                {
                    points.Remove(points[i]);
                }
            }
        }

        pathNodesSecondAStar = path.FindPath(startPoint, endPoint, points, triangles);

        for (int i = 0; i < points.Count; i++)
        {
            // Changer le j += 3 pour modifier les points a enlever
            for (int j = 0; j < pathNodesSecondAStar.Count; j += 3)
            {
                if (points[i].x == pathNodesSecondAStar[j].x && points[i].y == pathNodesSecondAStar[j].y)
                {
                    points.Remove(points[i]);
                }
            }
        }

        pathNodesThirdtAStar = path.FindPath(startPoint, endPoint, points, triangles);

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(pathNodesThirdtAStar[0].x, 0, pathNodesThirdtAStar[0].y);
    }

	void OnDrawGizmos()
	{
        //Gizmos.DrawWireCube(regionSize / 2, regionSize);
        if (sites != null)
        {
            for (int i = 0; i < sites.Count; i++)
            {
                Gizmos.color = Color.yellow;

                // For start point.
                if (i == sites.Count - 2)
                {
                    Gizmos.color = Color.red;
                }
                // For end point.
                else if (i == sites.Count - 1)
                {
                    Gizmos.color = Color.cyan;
                }

                Gizmos.DrawSphere(sites[i], displayRadius);
            }
        }

        Gizmos.color = Color.white;
        for (int i = 0; i < triangles.Count; i++)
		{
			Gizmos.DrawLine(triangles[i].v1.position, triangles[i].v2.position);
			Gizmos.DrawLine(triangles[i].v2.position, triangles[i].v3.position);
			Gizmos.DrawLine(triangles[i].v3.position, triangles[i].v1.position);
		}

        Gizmos.color = Color.green;
        for (int i = 0; i < pathNodesFirstAStar.Count - 1; i++)
        {
            Vector3 start = new Vector3(pathNodesFirstAStar[i].x, 0, pathNodesFirstAStar[i].y);
            Vector3 end = new Vector3(pathNodesFirstAStar[i+1].x, 0, pathNodesFirstAStar[i+1].y);
            Gizmos.DrawLine(start, end);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < pathNodesSecondAStar.Count - 1; i++)
        {
            Vector3 start = new Vector3(pathNodesSecondAStar[i].x, 0, pathNodesSecondAStar[i].y);
            Vector3 end = new Vector3(pathNodesSecondAStar[i + 1].x, 0, pathNodesSecondAStar[i + 1].y);
            Gizmos.DrawLine(start, end);
        }

        Gizmos.color = Color.blue;
        for (int i = 0; i < pathNodesThirdtAStar.Count - 1; i++)
        {
            Vector3 start = new Vector3(pathNodesThirdtAStar[i].x, 0, pathNodesThirdtAStar[i].y);
            Vector3 end = new Vector3(pathNodesThirdtAStar[i + 1].x, 0, pathNodesThirdtAStar[i + 1].y);
            Gizmos.DrawLine(start, end);
        }
    }
}
