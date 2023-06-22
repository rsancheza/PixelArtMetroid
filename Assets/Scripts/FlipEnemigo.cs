using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipEnemigo : MonoBehaviour
{

    private SpriteRenderer sprite;
    private float posicionxAnterior;

    void Start()
    {
        posicionxAnterior=transform.parent.position.x;
        sprite=GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        sprite.flipX = posicionxAnterior < transform.position.x;
        posicionxAnterior = transform.position.x;
    }
}
