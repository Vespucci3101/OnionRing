using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestMenu : MonoBehaviour
{
    PlayerStats playerStats;
    public float restSiteHealing = 75f;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = PlayerStats.GetInstance();

        if (playerStats == null) return;
        playerStats.player.currentHealt += restSiteHealing;

        // Can't heal more than the max player health
        if (playerStats.player.currentHealt > playerStats.player.health)
        {
            playerStats.player.currentHealt = playerStats.player.health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMap()
    {
        // Map scene
        SceneManager.LoadScene(3);
    }
}
