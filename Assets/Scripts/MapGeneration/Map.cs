using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton class, only want one map at the same time.
// Only created and accessed in MapGenerator.

public class Map
{
    private static Map instance;

    public float radius = 3;
    public Vector2 regionSize = new Vector2(30, 30);
    public int rejectionSamples = 30;
    public float displayRadius = 1;

    public List<PathNode> totalPoints;
    public PathNode currentNode;
    public GameObject playerSelectorCylinder;

    public bool isMapGenerated = false;

    List<Vector2> points;
    List<Vector3> sites;
    List<Vector3> points3D;

    List<Triangle> triangles;

    Pathfinding path;
    List<PathNode> pathNodesFirstAStar;
    List<PathNode> pathNodesSecondAStar;
    List<PathNode> pathNodesThirdAStar;

    public List<Vector2> mapNodesPositions;
    public List<MapNode> mapNodes;

    // Use for the battlePhase enemy
    public Enemy currentEnemy;
    public List<Enemy> enemyList;

    public Dictionary<Vector2, Enemy> mapEnemies;

    Map()
    {
        triangles = new List<Triangle>();
        sites = new List<Vector3>();
        points = new List<Vector2>();
        points3D = new List<Vector3>();
        totalPoints = new List<PathNode>();
        path = new Pathfinding();
        pathNodesFirstAStar = new List<PathNode>();
        pathNodesSecondAStar = new List<PathNode>();
        pathNodesThirdAStar = new List<PathNode>();
        mapNodesPositions = new List<Vector2>();
        mapNodes = new List<MapNode>();
        currentEnemy = new Enemy();
        enemyList = new List<Enemy>();
        mapEnemies = new Dictionary<Vector2, Enemy>();

        CreateEnemies();
    }

    public static Map GetMapInstance()
    {
        if (instance == null)
        {
            instance = new Map();
        }
        return instance;
    }

    public void LoadMap()
    {
        LoadMapNodes();

        CreateCylinderForPath(pathNodesFirstAStar);
        CreateCylinderForPath(pathNodesSecondAStar);
        CreateCylinderForPath(pathNodesThirdAStar);

        CreatePlayerSelectorCylinder();
        CreateStartAndEndSelectorCylinder();
    }

    public void GenerateMap()
    {
        isMapGenerated = true;
        points.Clear();
        sites.Clear();
        points3D.Clear();
        triangles.Clear();

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

        pathNodesFirstAStar.Clear();
        pathNodesSecondAStar.Clear();
        pathNodesThirdAStar.Clear();
        totalPoints.Clear();

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

        pathNodesThirdAStar = path.FindPath(startPoint, endPoint, points, triangles);

        totalPoints.AddRange(pathNodesFirstAStar);
        totalPoints.AddRange(pathNodesSecondAStar);
        totalPoints.AddRange(pathNodesThirdAStar);

        currentNode = totalPoints[0];

        CreateMapNodes();

        CreateCylinderForPath(pathNodesFirstAStar);
        CreateCylinderForPath(pathNodesSecondAStar);
        CreateCylinderForPath(pathNodesThirdAStar);

        CreatePlayerSelectorCylinder();
        CreateStartAndEndSelectorCylinder();
    }

    void LoadMapNodes()
    {
        foreach (MapNode mapNode in mapNodes)
        {
            mapNode.DrawNode(mapNode.position.x, mapNode.position.y);
        }
    }

    void CreateMapNodes()
    {
        mapNodesPositions = new List<Vector2>();
        mapNodes = new List<MapNode>();

        for (int i = 0; i < pathNodesFirstAStar.Count; i++)
        {
            MapNode mapNode = new MapNode();

            if (i == 0) mapNode = new StartNode(pathNodesFirstAStar[i].x, pathNodesFirstAStar[i].y);
            else if (i == pathNodesFirstAStar.Count - 1)
            {
                mapNode = new BossNode(pathNodesFirstAStar[i].x, pathNodesFirstAStar[i].y);
                if (!mapEnemies.ContainsKey(mapNode.position)) mapEnemies.Add(mapNode.position, enemyList[Random.Range(0, enemyList.Count)]);
            }
            else if (i != 0) mapNode = MapRules(mapNodes[mapNodes.Count - 1], pathNodesFirstAStar[i].x, pathNodesFirstAStar[i].y);

            mapNodes.Add(mapNode);
            mapNodesPositions.Add(new Vector2(pathNodesFirstAStar[i].x, pathNodesFirstAStar[i].y));
        }

        for (int i = 1; i < pathNodesSecondAStar.Count - 1; i++)
        {
            MapNode mapNode = new MapNode();

            Vector2 nodePosition = new Vector2(pathNodesSecondAStar[i].x, pathNodesSecondAStar[i].y);

            if (mapNodesPositions.Contains(nodePosition)) continue;

            Vector2 previousNodePosition = new Vector2(pathNodesSecondAStar[i - 1].x, pathNodesSecondAStar[i - 1].y);
            int previousIdx = mapNodesPositions.IndexOf(previousNodePosition);
            mapNode = MapRules(mapNodes[previousIdx], pathNodesSecondAStar[i].x, pathNodesSecondAStar[i].y);

            mapNodes.Add(mapNode);
            mapNodesPositions.Add(new Vector2(pathNodesSecondAStar[i].x, pathNodesSecondAStar[i].y));
        }

        for (int i = 1; i < pathNodesThirdAStar.Count - 1; i++)
        {
            MapNode mapNode = new MapNode();

            Vector2 nodePosition = new Vector2(pathNodesThirdAStar[i].x, pathNodesThirdAStar[i].y);
            if (mapNodesPositions.Contains(nodePosition)) continue;

            Vector2 previousNodePosition = new Vector2(pathNodesThirdAStar[i - 1].x, pathNodesThirdAStar[i - 1].y);
            int previousIdx = mapNodesPositions.IndexOf(previousNodePosition);
            mapNode = MapRules(mapNodes[previousIdx], pathNodesThirdAStar[i].x, pathNodesThirdAStar[i].y);

            mapNodes.Add(mapNode);
            mapNodesPositions.Add(new Vector2(pathNodesThirdAStar[i].x, pathNodesThirdAStar[i].y));
        }
    }

    MapNode MapRules(MapNode previousMapNode, float x, float y)
    {
        MapNode newMapNode = new MapNode();

        if (previousMapNode.nodeType == NodeType.NO_TYPE)
        {
            newMapNode = new EnemyNode(x, y);
            if (!mapEnemies.ContainsKey(new Vector2(x, y))) mapEnemies.Add(new Vector2(x, y), enemyList[Random.Range(0, enemyList.Count)]);
        }
        else if (previousMapNode.nodeType == NodeType.ENEMY_TYPE
                || previousMapNode.nodeType == NodeType.ELITE_TYPE
                || previousMapNode.nodeType == NodeType.TREASURE_TYPE)
        {
            int randomType = Random.Range(0, 4);
            switch (randomType)
            {
                case 0:
                    newMapNode = new EnemyNode(x, y);
                    if (!mapEnemies.ContainsKey(new Vector2(x, y))) mapEnemies.Add(new Vector2(x, y), enemyList[Random.Range(0, enemyList.Count)]);
                    break;

                case 1:
                    newMapNode = new ShopNode(x, y);
                    break;

                case 2:
                    newMapNode = new TreasureNode(x, y);
                    break;

                case 3:
                    newMapNode = new RestNode(x, y);
                    break;

                case 4:
                    newMapNode = new EliteNode(x, y);
                    break;
            }
        }
        else if (previousMapNode.nodeType == NodeType.SHOP_TYPE || previousMapNode.nodeType == NodeType.REST_TYPE)
        {
            //int randomType = Random.Range(0, 2);
            int randomType = 0;
            switch (randomType)
            {
                case 0:
                    newMapNode = new EnemyNode(x, y);
                    if (!mapEnemies.ContainsKey(new Vector2(x, y))) mapEnemies.Add(new Vector2(x, y), enemyList[Random.Range(0, enemyList.Count)]);
                    break;

                case 1:
                    newMapNode = new EliteNode(x, y);
                    break;
            }
        }

        return newMapNode;
    }

    void CreateStartAndEndSelectorCylinder()
    {
        GameObject startCylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        startCylinder.GetComponent<Renderer>().material.color = Color.green;
        startCylinder.transform.position = new Vector3(totalPoints[0].x, -0.7f, totalPoints[0].y);
        startCylinder.transform.localScale = new Vector3(1, 0.05f, 1);

        GameObject endCylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        endCylinder.GetComponent<Renderer>().material.color = Color.red;
        endCylinder.transform.position = new Vector3(totalPoints[totalPoints.Count - 1].x, -0.7f, totalPoints[totalPoints.Count - 1].y);
        endCylinder.transform.localScale = new Vector3(1, 0.05f, 1);
    }

    void CreatePlayerSelectorCylinder()
    {
        playerSelectorCylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        playerSelectorCylinder.GetComponent<Renderer>().material.color = Color.yellow;
        playerSelectorCylinder.transform.position = new Vector3(currentNode.x, -0.7f, currentNode.y);
        playerSelectorCylinder.transform.localScale = new Vector3(1, 0.05f, 1);
    }

    void CreateCylinderForPath(List<PathNode> path)
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.GetComponent<Renderer>().material.color = Color.gray;
            cylinder.transform.position = new Vector3((path[i].x + path[i + 1].x) / 2, 0, (path[i].y + path[i + 1].y) / 2);

            float yRotation = Mathf.Rad2Deg * Mathf.Atan((path[i + 1].y - path[i].y) / (path[i + 1].x - path[i].x));
            cylinder.transform.rotation = Quaternion.Euler(0, 180 - yRotation, 90);

            float cylDist = Mathf.Sqrt(Mathf.Pow((path[i + 1].x - path[i].x), 2) + Mathf.Pow((path[i + 1].y - path[i].y), 2));
            cylinder.transform.localScale = new Vector3(0.3f, cylDist / 4, 0.3f);
        }
    }

    public List<PathNode> GetPathNodeNeighbours(PathNode currentNode)
    {
        List<PathNode> neighbours = new List<PathNode>();

        for (int i = 0; i < pathNodesFirstAStar.Count; i++)
        {
            if (pathNodesFirstAStar[i].previousNode == null) continue;
            if (pathNodesFirstAStar[i].previousNode.x == currentNode.x && pathNodesFirstAStar[i].previousNode.y == currentNode.y)
            {
                neighbours.Add(pathNodesFirstAStar[i]);
            }
        }

        for (int i = 0; i < pathNodesSecondAStar.Count; i++)
        {
            if (pathNodesSecondAStar[i].previousNode == null) continue;
            if (pathNodesSecondAStar[i].previousNode.x == currentNode.x && pathNodesSecondAStar[i].previousNode.y == currentNode.y)
            {
                neighbours.Add(pathNodesSecondAStar[i]);
            }
        }

        for (int i = 0; i < pathNodesThirdAStar.Count; i++)
        {
            if (pathNodesThirdAStar[i].previousNode == null) continue;
            if (pathNodesThirdAStar[i].previousNode.x == currentNode.x && pathNodesThirdAStar[i].previousNode.y == currentNode.y)
            {
                neighbours.Add(pathNodesThirdAStar[i]);
            }
        }

        return neighbours;
    }

    public void SelectNode(PathNode nextNode)
    {
        currentNode = nextNode;

        Vector2 position = new Vector2(nextNode.x, nextNode.y);
        if (mapNodesPositions.Contains(position))
        {
            int idx = mapNodesPositions.IndexOf(position);

            if (mapNodes[idx].nodeType == NodeType.ENEMY_TYPE)
            {
                currentEnemy = mapEnemies[mapNodes[idx].position];
            }
            else if (mapNodes[idx].nodeType == NodeType.BOSS_TYPE)
            {
                currentEnemy = mapEnemies[mapNodes[idx].position];
                isMapGenerated = false;
            }

            mapNodes[idx].SelectNode();

            LoadScene(mapNodes[idx].nodeType);
        }
    }

    void LoadScene(int nodeType)
    {
        switch (nodeType)
        {
            case NodeType.ENEMY_TYPE:
                SceneManager.LoadScene(2);
                break;

            case NodeType.BOSS_TYPE:
                SceneManager.LoadScene(2);
                break;

            case NodeType.REST_TYPE:
                SceneManager.LoadScene(4);
                break;

            case NodeType.SHOP_TYPE:
                SceneManager.LoadScene(5);
                break;

            case NodeType.TREASURE_TYPE:
                SceneManager.LoadScene(6);
                break;
        }
    }

    void CreateEnemies()
    {
        // Order important

        //enemyList.Add(new TutorialBoiBad());
        enemyList.Add(new Poliwags());
        enemyList.Add(new Mario());
        enemyList.Add(new Luigi());
        enemyList.Add(new Samus());
        enemyList.Add(new Kirby());
        enemyList.Add(new DuckHunt());
        enemyList.Add(new GameAndWatch());
        enemyList.Add(new Sonic());
        enemyList.Add(new Kratos());
        enemyList.Add(new Link());
        enemyList.Add(new MegaMan());
        enemyList.Add(new TetrisCross());
        enemyList.Add(new TomNook());
    }

    public MapNode GetMapnodeFromPath(PathNode pathNode)
    {
        Vector2 position = new Vector2(pathNode.x, pathNode.y);
        if (mapNodesPositions.Contains(position))
        {
            int idx = mapNodesPositions.IndexOf(position);
            return mapNodes[idx];
        }
        return null;
    }
}
