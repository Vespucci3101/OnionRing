using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pultergust : Attack
{
    public Pultergust()
    {
        difficulty = "Easy";
        name = "Pultergust G-00";
        baseDamage = 50;
        criticalDamage = 25;
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
