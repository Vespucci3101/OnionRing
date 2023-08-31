using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Sonic : Enemy
{
    public Sonic()
    {
        name = "Sonic";
        health = 300;
        currentHealt = health;

        InitializeAvatar();
    }

    override public void InitializeAttacks()
    {
        attacks = new Attack[4] { new SpinDash(), new SuperSonic(), new Slap(), new SpinDash() };

        for (int i = 0; i < attacks.Length; i++)
        {
            attacks[i].buttonsCreator = buttonsCreator;
            attacks[i].battlePhase = battlePhase;
        }
    }

    public override void InitializeAvatar()
    {
        string imagePath = Application.dataPath + "/Materials/EnemyAvatars/Sonic.png";
        byte[] bytes = File.ReadAllBytes(imagePath);
        Texture2D loadtexture = new Texture2D(1, 1);
        loadtexture.LoadImage(bytes);

        avatar = Sprite.Create(loadtexture, new Rect(0.0f, 0.0f, loadtexture.width, loadtexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        avatarSize = new Vector2(200, 200);
        avatarShift = new Vector3(100, 0, 0);
    }
}
