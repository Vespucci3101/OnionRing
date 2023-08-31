using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCharacter
{
    public float health;
    public float currentHealt;

    public HealthCharacter()
    {
        health = 1;
        currentHealt = health;
    }

    public void TakeDamage(float damage) 
    {
        currentHealt -= damage;
    }
}
