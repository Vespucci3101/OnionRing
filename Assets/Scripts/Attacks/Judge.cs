using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : Attack
{
    public Judge()
    {
        difficulty = "Medium";
        name = "Judge";
        baseDamage = 25;
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

        // 1/9 chance to hit a 9
        int misfireChance = Random.Range(0, 9);
        if (misfireChance == 0) criticalDamage = 200;

        ReturnToBattle(baseDamage, criticalDamage, target, isAttacking);
    }
}
