using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsCreator : MonoBehaviour
{
    public GameObject blankButtonA;
    public GameObject blankButtonB;
    public GameObject blankButtonX;
    public GameObject blankButtonY;

    public Sprite buttonSpriteA;
    public Sprite buttonSpriteB;
    public Sprite buttonSpriteX;
    public Sprite buttonSpriteY;

    public ShowHideUI perfectShowHideUI;
    public ShowHideUI goodShowHideUI;
    public ShowHideUI badShowHideUI;

    GameObject canvas;
    CharacterInput characterInput;

    public void CreateButton(int buttonType, float buttonSpeed)
    {
        canvas = transform.GetChild(0).gameObject;
        characterInput = GameObject.Find("Player").GetComponent<CharacterInput>();

        float x;
        string gameObjectName = "Button";
        Sprite buttonSprite;

        switch (buttonType)
        {
            // ButtonX
            case 0:
                x = -302f;
                gameObjectName += "X";
                buttonSprite = buttonSpriteX;
                break;
            // ButtonY
            case 1:
                x = -103f;
                gameObjectName += "Y";
                buttonSprite = buttonSpriteY;
                break;
            // ButtonA
            case 2:
                x = 102f;
                gameObjectName += "A";
                buttonSprite = buttonSpriteA;
                break;
            // ButtonB
            case 3:
                x = 301f;
                gameObjectName += "B";
                buttonSprite = buttonSpriteB;
                break;
            // ButtonX
            default:
                x = -302f;
                gameObjectName += "X";
                buttonSprite = buttonSpriteX;
                break;
        }

        GameObject imgObject = new GameObject(gameObjectName);

        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.transform.SetParent(canvas.transform); // setting parent
        trans.localScale = Vector3.one;
        trans.sizeDelta = new Vector2(175, 175); // custom size
        trans.anchoredPosition = new Vector2(x, -655f); // setting position, will be on center

        Image image = imgObject.AddComponent<Image>();
        image.sprite = buttonSprite;
        imgObject.transform.SetParent(canvas.transform);

        MovingButtons movingButton = imgObject.AddComponent<MovingButtons>();
        movingButton.obj = imgObject;
        movingButton.buttonMoveSpeed = buttonSpeed;
        movingButton.perfectShowHideUI = perfectShowHideUI;
        movingButton.goodShowHideUI = goodShowHideUI;
        movingButton.badShowHideUI = badShowHideUI;
        movingButton.playerScore = characterInput.playerScore;

        switch (buttonType)
        {
            // ButtonX
            case 0:
                characterInput.buttonXs.Enqueue(movingButton);
                movingButton.blankButton = blankButtonX;
                break;
            // ButtonY
            case 1:
                characterInput.buttonYs.Enqueue(movingButton);
                movingButton.blankButton = blankButtonY;
                break;
            // ButtonA
            case 2:
                characterInput.buttonAs.Enqueue(movingButton);
                movingButton.blankButton = blankButtonA;
                break;
            // ButtonB
            case 3:
                characterInput.buttonBs.Enqueue(movingButton);
                movingButton.blankButton = blankButtonB;
                break;
            // ButtonX
            default:
                characterInput.buttonXs.Enqueue(movingButton);
                movingButton.blankButton = blankButtonX;
                break;
        }

        StartCoroutine(movingButton.MoveButton());
    }
}
