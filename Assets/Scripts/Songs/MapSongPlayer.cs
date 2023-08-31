using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MapSongPlayer : MonoBehaviour
{
    AudioSource audioSource;
    Map map;

    Dictionary<string, string> audioSources;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = new Dictionary<string, string>();

        audioSource = GameObject.Find("Music").GetComponent<AudioSource>();

        map = Map.GetMapInstance();
        if (map == null) return;

        InitializeAudioSources();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeAudioClip(PathNode node)
    {
        if (map == null) return;

        StartCoroutine(LoadAudio(node));
    }

    void InitializeAudioSources()
    {
        audioSources = new Dictionary<string, string>();
        // Order important, surely a better way to do this
        audioSources.Add(map.enemyList[0].name, "/Musics/Music/Pokemon.mp3");
        audioSources.Add(map.enemyList[1].name, "/Musics/Music/Mario64.mp3");
        audioSources.Add(map.enemyList[2].name, "/Musics/Music/Luigi.mp3");
        audioSources.Add(map.enemyList[3].name, "/Musics/Music/Samus.mp3");
        audioSources.Add(map.enemyList[4].name, "/Musics/Music/Kirby.mp3");
        audioSources.Add(map.enemyList[5].name, "/Musics/Music/DuckHunt.mp3");
        audioSources.Add(map.enemyList[6].name, "/Musics/Music/GameAndWatch.mp3");
        audioSources.Add(map.enemyList[7].name, "/Musics/Music/Sonic.mp3");
        audioSources.Add(map.enemyList[8].name, "/Musics/Music/GOW.mp3");
        audioSources.Add(map.enemyList[9].name, "/Musics/Music/Link.mp3");
        audioSources.Add(map.enemyList[10].name, "/Musics/Music/MegaMan.mp3");
        audioSources.Add(map.enemyList[11].name, "/Musics/Music/Tetris.mp3");
        audioSources.Add(map.enemyList[12].name, "/Musics/Music/AnimalCrossing.mp3");
    }

    IEnumerator LoadAudio(PathNode node)
    {
        audioSource.Stop();

        Vector2 nodePos = new Vector2(node.x, node.y);
        string fileName = "";
        if (map.mapEnemies.ContainsKey(nodePos))
        {
            fileName = audioSources[map.mapEnemies[nodePos].name];
        }
        else
        {
            MapNode currentMapnode = map.GetMapnodeFromPath(node);
            switch(currentMapnode.nodeType)
            {
                case NodeType.REST_TYPE:
                    fileName = "/Musics/Music/DS3.mp3";
                    break;

                case NodeType.TREASURE_TYPE:
                    fileName = "/Musics/Music/BosunBill.mp3";
                    break;

                case NodeType.SHOP_TYPE:
                    fileName = "/Musics/Music/HS.mp3";
                    break;

                case NodeType.START_TYPE:
                    break;

                case NodeType.NO_TYPE:
                    break;
            }
        }

        WWW request = GetAudioFromFile(fileName);
        yield return request;

        audioSource.clip = request.GetAudioClip();

        audioSource.Play();
        audioSource.loop = true;
    }

    WWW GetAudioFromFile(string filename)
    {
        WWW request = new WWW(Application.dataPath + filename);
        return request;
    }
}
