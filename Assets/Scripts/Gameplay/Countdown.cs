using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    SongPlayer songPlayer;
    AudioSource audioSource;

    public Image countdownImage;
    public GameObject countdownImageObj;

    public Sprite Sprite3;
    public Sprite Sprite2;
    public Sprite Sprite1;

    // Start is called before the first frame update
    void Start()
    {
        songPlayer = GameObject.Find("SongPlayer").GetComponent<SongPlayer>();
        audioSource = gameObject.GetComponent<AudioSource>();

        StartCoroutine(SongStartCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SongStartCountdown()
    {
        StartCoroutine(LoadAudio());
        countdownImageObj.SetActive(false);

        yield return new WaitForSeconds(0.75f);

        countdownImageObj.SetActive(true);

        countdownImage.sprite = Sprite3;

        yield return new WaitForSeconds(0.8f);

        countdownImage.sprite = Sprite2;

        yield return new WaitForSeconds(0.9f);

        countdownImage.sprite = Sprite1;

        yield return new WaitForSeconds(0.8f);

        countdownImageObj.SetActive(false);

        songPlayer.StartSong();
    }

    IEnumerator LoadAudio()
    {
        audioSource.Stop();

        WWW request = new WWW(Application.dataPath + "/Musics/SoundEffects/321Go.mp3");
        yield return request;

        audioSource.clip = request.GetAudioClip();

        audioSource.Play();
        audioSource.loop = false;
    }
}
