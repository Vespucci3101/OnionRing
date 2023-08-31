using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMenu : MonoBehaviour
{
    public GameObject character;
    public GameObject secondMenu;
    public GameObject firstMenu;

    Vector3 lastPos = new Vector3(100, -1041, 0);

    bool isFirstMenu = true;
    bool isMenuMoving = false;

    float timeElapsed;
    float lerpDuration = 5;

    private void Update()
    {
        if (isMenuMoving && timeElapsed < lerpDuration)
        {
            timeElapsed += Time.deltaTime;
            MoveToSecondMenu(timeElapsed);
        }
    }

    public void PlayGame()
    {
        if (isFirstMenu)
        {
            isMenuMoving = true;
            isFirstMenu = false;
            StartCoroutine(ChangeText());
        }
        else
        {
            // Map scene
            SceneManager.LoadScene(3);
        }
    }

    void MoveToSecondMenu(float timeElapsed)
    {
        float x = Mathf.Lerp(character.transform.position.x, lastPos.x, timeElapsed / lerpDuration);
        character.transform.position = new Vector3(x, character.transform.position.y, character.transform.position.z);
    }

    IEnumerator ChangeText()
    {
        firstMenu.SetActive(false);

        yield return new WaitForSeconds(0.25f);
        
        secondMenu.SetActive(true);
    }
}
