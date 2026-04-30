using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public static Hand Instance;

    public List<DisplayCard> cards = new List<DisplayCard>();
    public Transform shotOrigin;
    public int currentCardIndex = 0;

    public ShotPool pool;
    private AudioSource cardAudioSource;
    public AudioSource shotAudioSource;

    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        cardAudioSource = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        if (currentCardIndex < cards.Count)
        {
            if(cards[currentCardIndex].currentShotNumber != 0 )
            {
                // Shoot logic
                Shot shot = pool.GetShot();

                shot.transform.position = shotOrigin.position;
                shot.transform.rotation = shotOrigin.rotation;

                shot.Initialize(cards[currentCardIndex].card.shotData);
                shot.Fire();

                shotAudioSource.Play();

                cards[currentCardIndex].currentShotNumber--;
                cards[currentCardIndex].UpdateClip();
            }
            else
            {
                Reload(currentCardIndex);
                cards[currentCardIndex].UpdateClip();
                SwitchToNextCard();
            }
        }
        else
        {
            currentCardIndex = 0;
        }
    }

    public void Reload(int cardIndex)
    {
        cards[cardIndex].currentShotNumber = cards[cardIndex].clipSize;
        cardAudioSource.Play();
    }

    public void SwitchToNextCard()
    {
        currentCardIndex++;
    }
}
