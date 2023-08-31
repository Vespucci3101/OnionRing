using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNote
{
    public MusicNote(float x, float y)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(x, 0, y);
        sphere.GetComponent<Renderer>().material.color = Color.black;

        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.position = new Vector3(x + 0.3f, 0.75f, y);
        cylinder.transform.localScale = new Vector3(0.35f, 0.75f, 0.35f);
        cylinder.GetComponent<Renderer>().material.color = Color.black;
    }
}
