using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public string name;
    public string difficulty;
    public float baseDamage;
    public float criticalDamage;
    public ButtonsCreator buttonsCreator;
    public BattlePhase battlePhase;

    public const int BUTTONX_IDX = 0;
    public const int BUTTONY_IDX = 1;
    public const int BUTTONA_IDX = 2;
    public const int BUTTONB_IDX = 3;

    public Attack()
    {
        name = "";
        difficulty = "";
        baseDamage = 0;
        criticalDamage = 0;
    }

    virtual public IEnumerator Use(HealthCharacter target, bool isAttacking) { yield return new WaitForSeconds(1f); }

    public void ReturnToBattle(float damage, float criticalDamage, HealthCharacter target, bool isAttacking)
    {
        if (isAttacking)
        {
            battlePhase.ReturnToDefencePhase();
        }
        else
        {
            battlePhase.ReturnToAttackPhase();
        }
        battlePhase.DealDamageToCharacter(damage, criticalDamage, target, isAttacking);
    }
}
