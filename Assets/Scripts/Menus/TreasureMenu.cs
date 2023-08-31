using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TreasureMenu : MonoBehaviour
{
    public TMP_Text treasureMessage;

    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = PlayerStats.GetInstance();

        treasureMessage.text = LoadScenario();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMapScene()
    {
        // Map scene
        SceneManager.LoadScene(3);
    }

    string LoadScenario()
    {
        int scenario = Random.Range(0, 3);
        string message = "";
        switch (scenario)
        {
            case 0:
                message = "You found an healing potion that restore you 25 health";
                if (playerStats != null) playerStats.player.currentHealt += 25;
                break;

            case 1:
                message = "You found an old papyrus containing information about martial arts";
                if (playerStats != null) SetPlayerAttack(GetRandomAttack());
                    break;

            case 2:
                message = "You found an old pair of socks worth 5 coins";
                if (playerStats != null) playerStats.playerScore += 5;
                break;
        }
        return message;
    }

    Attack GetRandomAttack()
    {
        int attackIdx = Random.Range(0, 5);
        switch(attackIdx)
        {
            case 0:
                return new PayLoan();

            case 1:
                return new ChargeShot();

            case 2:
                return new Dunk();

            case 3:
                return new Fireball();

            case 4:
                return new AxeThrow();

            default:
                return null;
        }
    }

    void SetPlayerAttack(Attack attack)
    {
        if (playerStats.attacks == null) return;

        bool attackChanged = false;
        for (int i = 0; i < playerStats.attacks.Length; i++)
        {
            if (attackChanged) return;
            if (playerStats.attacks[i] == null)
            {
                playerStats.attacks[i] = attack;
                attackChanged = true;
            }
        }

        if (attackChanged) return;

        // Replacing an attack randomly for now
        playerStats.attacks[Random.Range(0, playerStats.attacks.Length)] = attack;
    }
}
