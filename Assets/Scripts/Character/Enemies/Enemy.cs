using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : HealthCharacter
{
    public string name;
    public Attack[] attacks;
    public ButtonsCreator buttonsCreator;
    public BattlePhase battlePhase;
    public Sprite avatar;
    public Vector2 avatarSize;
    public Vector3 avatarShift;

    public Enemy() { }

    virtual public void InitializeAttacks() { }
    virtual public void InitializeAvatar() { }
}
