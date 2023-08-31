using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    private static PlayerStats instance;

    public float playerScore;
    public Attack[] attacks;
    public Player player;

    PlayerStats()
    {
        player = new Player();
        attacks = new Attack[4] { new Slap(), null, null, null };
    }

    public static PlayerStats GetInstance()
    {
        if (instance == null)
        {
            instance = new PlayerStats();
        }
        return instance;
    }

    public void InitializeAttacks(ButtonsCreator buttonsCreator, BattlePhase battlePhase)
    {
        for (int i = 0; i < attacks.Length; i++)
        {
            if (attacks[i] == null) continue;
            attacks[i].buttonsCreator = buttonsCreator;
            attacks[i].battlePhase = battlePhase;
        }
    }
}
