using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideUI: MonoBehaviour
{
    public GameObject UIObject;

    // Start is called before the first frame update
    void Start()
    {
        UIObject.SetActive(false);
    }

    public void HideUI()
    {
        UIObject.SetActive(false);
    }

    public void ShowUI()
    {
        UIObject.SetActive(true);
    }
}
