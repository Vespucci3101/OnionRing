using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingButtons : MonoBehaviour
{
    public GameObject obj;
    public GameObject blankButton;
    public PlayerScore playerScore;

    public ShowHideUI perfectShowHideUI;
    public ShowHideUI goodShowHideUI;
    public ShowHideUI badShowHideUI;

    public float buttonMoveSpeed = 500f;

    public float perfectDistance = 15f;

    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        perfectShowHideUI = GameObject.Find("PerfectUI").transform.GetChild(0).GetComponent<ShowHideUI>();
        goodShowHideUI = GameObject.Find("GoodUI").transform.GetChild(0).GetComponent<ShowHideUI>();
        badShowHideUI = GameObject.Find("BadUI").transform.GetChild(0).GetComponent<ShowHideUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving) obj.transform.position += Vector3.up * 400f * Time.deltaTime;
    }

    public IEnumerator MoveButton()
    {
        isMoving = true;
        PlayerScore.buttonCounter++;

        yield return new WaitWhile(() => obj.transform.position.y < 1080);

        isMoving = false;
        Destroy(obj);
    }

    public void CheckButtonTiming()
    {
        perfectShowHideUI.HideUI();
        goodShowHideUI.HideUI();
        badShowHideUI.HideUI();

        if (obj.transform.position.y >= blankButton.transform.position.y - perfectDistance
            && obj.transform.position.y <= blankButton.transform.position.y + perfectDistance)
        {
            perfectShowHideUI.ShowUI();
            playerScore.AddPerfectScore();
            playerScore.IncrementCombo();
        }
        else if ((obj.transform.position.y <= blankButton.transform.position.y - perfectDistance 
                && obj.transform.position.y >= blankButton.transform.position.y - (perfectDistance * 2))
                ||
                (obj.transform.position.y >= blankButton.transform.position.y + perfectDistance
                && obj.transform.position.y <= blankButton.transform.position.y + (perfectDistance * 2)))
        {
            goodShowHideUI.ShowUI();
            playerScore.AddGoodScore();
            playerScore.IncrementCombo();
        }
        else
        {
            badShowHideUI.ShowUI();
            playerScore.BreakCombo();
        }
        obj.transform.position = new Vector3(obj.transform.position.x, 1080, obj.transform.position.z);
    }
}
