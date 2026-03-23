using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public int clipSize;
    public ShotData shotData;
    public Color cardColor;
    public Shader cardShader;
    //public Sticker sticker;
}
