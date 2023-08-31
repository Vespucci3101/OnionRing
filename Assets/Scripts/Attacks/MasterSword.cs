using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSword : Attack
{
    public MasterSword()
    {
        difficulty = "Hard";
        name = "Master Sword";
        baseDamage = 200;
        criticalDamage = 50;
    }

    public override IEnumerator Use(HealthCharacter target, bool isAttacking)
    {
        float songSpeed = 0.55f;
        float secondsBetweenButtons = 0.4f;

        yield return new WaitForSeconds(secondsBetweenButtons);

        for (int i = 0; i < 10; i++)
        {
            buttonsCreator.CreateButton(Random.Range(0, 4), songSpeed);

            yield return new WaitForSeconds(secondsBetweenButtons);
        }

        yield return new WaitForSeconds(5f);

        ReturnToBattle(baseDamage, criticalDamage, target, isAttacking);
    }
}
