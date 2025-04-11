using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SculptureData", menuName = "Sculpture Stats", order = 1)]
public class SculptureStats : ScriptableObject
{
    public string ID;

    public string sculptureName;

    public string description;
    public Texture2D image;
}

