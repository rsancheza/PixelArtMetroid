using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDatosJuego : MonoBehaviour
{

    private int puntuacion;
    private bool ganador;

    public int Puntuacion { get => puntuacion; set => puntuacion = value; }
    public bool Ganador { get => ganador; set => ganador = value; }

    private void Awake()
    {

        int numDistancias=FindObjectsOfType<ControlDatosJuego>().Length;

        if (numDistancias != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }

    }
}
