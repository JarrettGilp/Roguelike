using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPool : MonoBehaviour
{
    public Shot shotPrefab;
    private Queue<Shot> pool = new Queue<Shot>();

    public Shot GetShot()
    {
        if (pool.Count > 0)
        {
            Shot shot = pool.Dequeue();
            shot.gameObject.SetActive(true);
            return shot;
        }

        return Instantiate(shotPrefab);
    }

    public void ReturnShot(Shot shot)
    {
        shot.ResetShot();
        shot.gameObject.SetActive(false);
        pool.Enqueue(shot);
    }
}
