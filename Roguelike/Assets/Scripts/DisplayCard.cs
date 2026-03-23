using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

public class DisplayCard : MonoBehaviour
{
    public Card card;
    public TMP_Text currentClipTextField;
    public Image cardImage;
    public int currentShotNumber;
    public int clipSize;

    private void Start()
    {
        clipSize = card.clipSize;
        UpdateClip();
    }

    public void UpdateClip()
    {
        currentClipTextField.text = currentShotNumber.ToString() + "/" + clipSize.ToString();
    }

    public void UpdateClipSize(int newClipSize)
    {
        clipSize = newClipSize;
        UpdateClip();
    }

}
