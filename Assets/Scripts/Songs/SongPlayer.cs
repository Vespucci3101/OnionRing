using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongPlayer : MonoBehaviour
{
    Map map;

    Dictionary<string, Song> songsList;
    Dictionary<string, string> audioSources;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = new Dictionary<string, string>();
        audioSource = GameObject.Find("Music").GetComponent<AudioSource>();

        map = Map.GetMapInstance();

        if (map == null) return;

        songsList = new Dictionary<string, Song>();

        GameObject buttonsCreatorObject = GameObject.Find("NeutralUICircles");
        GameObject battlePhaseObject = GameObject.Find("BattlePhase");

        if (buttonsCreatorObject == null || battlePhaseObject == null) return;

        ButtonsCreator buttonsCreator = buttonsCreatorObject.GetComponent<ButtonsCreator>();
        BattlePhase battlePhase = battlePhaseObject.GetComponent<BattlePhase>();

        if (buttonsCreator == null || battlePhase == null) return;

        InitializeAudioSources();

        CreateSongs(buttonsCreator, battlePhase);

        StartCoroutine(LoadAudio(map.currentNode));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartSong()
    {
        audioSource.Play();
        StartCoroutine(songsList[map.currentEnemy.name].PlaySong());
    }

    void CreateSongs(ButtonsCreator buttonsCreator, BattlePhase battlePhase)
    {
        for (int i = 0; i < map.enemyList.Count; i++)
        {
            songsList.Add(map.enemyList[i].name, new Song1(battlePhase, buttonsCreator));
        }
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

        WWW request = GetAudioFromFile(audioSources[map.mapEnemies[new Vector2(node.x, node.y)].name]);
        yield return request;

        audioSource.clip = request.GetAudioClip();
        
        audioSource.loop = true;
    }

    WWW GetAudioFromFile(string filename)
    {
        WWW request = new WWW(Application.dataPath + filename);
        return request;
    }
}
