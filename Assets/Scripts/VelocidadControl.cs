using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocidadControl : MonoBehaviour
{
    public int cantidad;
    public float velocidad;
    public Vector3 posicionFin;
    public AudioClip powerUpFX;

    private Vector3 posicionInicio;
    private bool direccionDestino;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ControlJugador>().IncrementarVelocidad();
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(powerUpFX);
            Destroy(gameObject);
        }
    }
}
