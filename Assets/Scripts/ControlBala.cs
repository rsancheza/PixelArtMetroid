using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBala : MonoBehaviour
{
    public int velocidad = 20;
    public int daño = 2;

    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        Destroy(gameObject, 0.5f);
    }
}