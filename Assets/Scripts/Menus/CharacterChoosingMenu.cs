using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.IO;

public class CharacterChoosingMenu : MonoBehaviour
{
    EventSystem eventSystem;

    public Image characterAvatar;

    public TMP_Text characterName;
    public TMP_Text characterHealth;
    public TMP_Text characterSpecialSkill;

    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        playerStats = PlayerStats.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        SetSelectedCharacter();
    }

    public void ChooseDK()
    {
        if (playerStats == null) return;

        playerStats.player.health = 400;
        playerStats.player.currentHealt = 400;

        // Map scene
        SceneManager.LoadScene(3);
    }

    public void ChooseCaptainFalcon()
    {
        if (playerStats == null && playerStats.attacks == null) return;

        playerStats.attacks[1] = new FalconPunch();

        // Map scene
        SceneManager.LoadScene(3);
    }

    public void ChooseLucario()
    {
        if (playerStats == null) return;

        //damage with less health

        // Map scene
        SceneManager.LoadScene(3);
    }

    public void ChooseOlimar()
    {
        if (playerStats == null) return;

        //add pikmins (5)

        // Map scene
        SceneManager.LoadScene(3);
    }

    void SetSelectedCharacter()
    {
        GameObject selectedButton = eventSystem.currentSelectedGameObject;
        string filePath = "";

        switch(selectedButton.name)
        {
            case "DK":
                filePath = "/Materials/PlayerAvatar/DK.png";
                characterName.text = "Donkey Kong";
                characterHealth.text = "Health : 400";
                characterSpecialSkill.text = "Starts with more health";
                break;

            case "Captain":
                filePath = "/Materials/PlayerAvatar/CaptainFalcon.png";
                characterName.text = "Captain Falcon";
                characterHealth.text = "Health : 300";
                characterSpecialSkill.text = "Starts with the Falcon Punch attack";
                break;

            case "Lucario":
                filePath = "/Materials/PlayerAvatar/Lucario.png";
                characterName.text = "Lucario";
                characterHealth.text = "Health : 300";
                characterSpecialSkill.text = "Deals more damage the less health you have";
                break;

            case "Olimar":
                filePath = "/Materials/PlayerAvatar/Olimar.png";
                characterName.text = "Olimar";
                characterHealth.text = "Health : 250";
                characterSpecialSkill.text = "You have pikmins, which helps you deal more damage the more you have. Taking damage make you lose pikmins and can only be retrieve when resting";
                break;
        }

        string imagePath = Application.dataPath + filePath;
        byte[] bytes = File.ReadAllBytes(imagePath);
        Texture2D loadtexture = new Texture2D(1, 1);
        loadtexture.LoadImage(bytes);
        characterAvatar.sprite = Sprite.Create(loadtexture, new Rect(0.0f, 0.0f, loadtexture.width, loadtexture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}
