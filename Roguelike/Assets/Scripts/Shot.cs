using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shot: MonoBehaviour
{
    public float speed;
    public string element;
    public bool isChargeable;
    public Color shotColor;
    public SpriteRenderer shotSpriteRenderer;
    public Material shotShader;
    //public Renderer rend;
    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(ShotData data)
    {
        speed = data.speed;
        element = data.element;
        isChargeable = data.isChargeable;
        shotSpriteRenderer.sprite = data.shotSprite;
        shotSpriteRenderer.color = data.shotColor;

        //rend.material = data.shotMaterial;
        //rend.material.color = data.shotColor;
    }

    public void Fire()
    {
        rb.velocity = transform.right * speed;
    }

    public void ResetShot()
    {
        rb.velocity = Vector2.zero;
    }
}
