using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class MapMenu : MonoBehaviour
{
    public TMP_Text playerScore;
    public TMP_Text playerHealth;

    public TMP_Text nodeTitle;
    public TMP_Text nodeGame;
    public TMP_Text nodeSongTitle;
    public Image nodeImage;

    PlayerStats playerStats;
    Map map;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = PlayerStats.GetInstance();
        map = Map.GetMapInstance();

        if (playerStats == null) return;
        playerScore.text = playerStats.playerScore.ToString();
        playerHealth.text = playerStats.player.currentHealt.ToString() + " / " + playerStats.player.health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeNodeUI(PathNode node)
    {
        MapNode mapNode = map.GetMapnodeFromPath(node);
        switch (mapNode.nodeType)
        {
            case NodeType.ENEMY_TYPE:
                nodeTitle.text = "Enemy";
                nodeImage.sprite = SetEnemyNodeImage(map.mapEnemies[mapNode.position].name);
                break;

            case NodeType.REST_TYPE:
                nodeTitle.text = "Rest site";
                nodeImage.sprite = SetOtherNodeImage(NodeType.REST_TYPE);
                break;

            case NodeType.TREASURE_TYPE:
                nodeTitle.text = "Treasure";
                nodeImage.sprite = SetOtherNodeImage(NodeType.TREASURE_TYPE);
                break;

            case NodeType.SHOP_TYPE:
                nodeTitle.text = "Shop";
                nodeImage.sprite = SetOtherNodeImage(NodeType.SHOP_TYPE);
                break;
        }
    }

    Sprite SetEnemyNodeImage(string enemyName)
    {
        string imagePath = Application.dataPath + GetEnemyFileName(enemyName);
        byte[] bytes = File.ReadAllBytes(imagePath);
        Texture2D loadtexture = new Texture2D(1, 1);
        loadtexture.LoadImage(bytes);

        return Sprite.Create(loadtexture, new Rect(0.0f, 0.0f, loadtexture.width, loadtexture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    Sprite SetOtherNodeImage(int nodeType)
    {
        string fileName = "";
        if (nodeType == NodeType.REST_TYPE) fileName = "/Materials/NodesImages/RestSite.png";
        else if (nodeType == NodeType.TREASURE_TYPE) fileName = "/Materials/NodesImages/TreasureSite.png";
        else if (nodeType == NodeType.SHOP_TYPE) fileName = "/Materials/NodesImages/ShopSite.png";

        string imagePath = Application.dataPath + fileName;
        byte[] bytes = File.ReadAllBytes(imagePath);
        Texture2D loadtexture = new Texture2D(1, 1);
        loadtexture.LoadImage(bytes);

        return Sprite.Create(loadtexture, new Rect(0.0f, 0.0f, loadtexture.width, loadtexture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    string GetEnemyFileName(string enemyName)
    {
        string fileName = "";
        switch(enemyName)
        {
            case "Poliwags":
                fileName = "/Materials/NodesImages/PokemonRed.jpg";
                break;

            case "Mario":
                fileName = "/Materials/NodesImages/Mario64.jpg";
                break;

            case "Luigi":
                fileName = "/Materials/NodesImages/LuigiMansion.jpg";
                break;

            case "Samus":
                fileName = "/Materials/NodesImages/Metroid.jpg";
                break;

            case "Kirby":
                fileName = "/Materials/NodesImages/KirbyDreamLand.jpg";
                break;

            case "Duck Hunt":
                fileName = "/Materials/NodesImages/DuckHunt.jpeg";
                break;

            case "Mr Game And Watch":
                fileName = "/Materials/NodesImages/GameAndWatch.jpg";
                break;

            case "Sonic":
                fileName = "/Materials/NodesImages/SonicHedgehog.jpg";
                break;

            case "Kratos":
                fileName = "/Materials/NodesImages/GOW.jpg";
                break;

            case "Link":
                fileName = "/Materials/NodesImages/ZeldaOOT.jpg";
                break;

            case "Mega Man":
                fileName = "/Materials/NodesImages/MegaManX.jpg";
                break;

            case "Tetris Cross":
                fileName = "/Materials/NodesImages/TetrisNES.jpg";
                break;

            case "Tom Nook":
                fileName = "/Materials/NodesImages/AnimalCrossing.jpg";
                break;

            default:
                break;
        }
        return fileName;
    }
}
