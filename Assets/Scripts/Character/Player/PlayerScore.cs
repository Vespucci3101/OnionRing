using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public TMP_Text playerScoreText;

    public Image img1;
    public Image img2;
    public Image img3;

    public Sprite sprite0;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;
    public Sprite sprite7;
    public Sprite sprite8;
    public Sprite sprite9;

    public float perfectScore = 100f;
    public float goodScore = 50f;

    public static int perfectCounter;
    public static int goodOrBetterCounter;
    public static int buttonCounter;

    public float playerScore;
    int playerCombo;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0f;
        playerCombo = 0;

        img1.sprite = sprite0;
        img2.sprite = sprite0;
        img3.sprite = sprite0;
    }

    // Update is called once per frame
    void Update()
    {
        playerScoreText.text = playerScore.ToString();
    }

    public void AddPerfectScore()
    {
        playerScore += perfectScore;
        perfectCounter++;
        goodOrBetterCounter++;
    }

    public void AddGoodScore()
    {
        playerScore += goodScore;
        goodOrBetterCounter++;
    }

    public void IncrementCombo()
    {
        playerCombo++;

        ChangeComboSprites();
    }

    public void BreakCombo()
    {
        playerCombo = 0;

        ChangeComboSprites();
    }

    void ChangeComboSprites()
    {
        img1.sprite = GetImageNumber(2);
        img2.sprite = GetImageNumber(1);
        img3.sprite = GetImageNumber(0);
    }

    Sprite GetImageNumber(int imageIdx)
    {
        float combo = 0;

        if (imageIdx == 1)
        {
            combo = playerCombo % 100;
        }
        else if (imageIdx == 0)
        {
            combo = playerCombo % 10;
        }

        float number = Mathf.Floor(combo / Mathf.Pow(10, imageIdx));

        return number switch
        {
            0 => sprite0,
            1 => sprite1,
            2 => sprite2,
            3 => sprite3,
            4 => sprite4,
            5 => sprite5,
            6 => sprite6,
            7 => sprite7,
            8 => sprite8,
            9 => sprite9,
            _ => sprite0,
        };
    }
}
