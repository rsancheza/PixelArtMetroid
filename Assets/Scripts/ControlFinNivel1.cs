using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlFinNivel1 : MonoBehaviour
{
    public TextMeshProUGUI mensajeFinal;
    private ControlDatosJuego controlDatos;

    private void Start()
    {
        controlDatos = GameObject.Find("DatosJuego").GetComponent<ControlDatosJuego>();
        string mensaje = (controlDatos.Ganador) ? "Has Ganado" : "Has Perdido";
        if (controlDatos.Ganador)
        {
            mensaje += "\n \n Puntuación: " + controlDatos.Puntuacion;
        }
        mensajeFinal.text = mensaje;
    }

}
