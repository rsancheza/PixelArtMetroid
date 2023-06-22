using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpControl : MonoBehaviour
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
            collision.gameObject.GetComponent<ControlJugador>().IncrementarPuntos(cantidad);
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(powerUpFX);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        posicionInicio = transform.position;
        direccionDestino = true;
    }


    void Update()
    {
        DesplazarPowerUp();
    }

    private void DesplazarPowerUp()
    {
        Vector3 posicionDestino = direccionDestino ? posicionFin : posicionInicio;
        transform.position = Vector3.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);
        if (transform.position == posicionFin)
        {
            direccionDestino = false;
        }

        if (transform.position == posicionInicio)
        {
            direccionDestino = true;
        }
    }

}
