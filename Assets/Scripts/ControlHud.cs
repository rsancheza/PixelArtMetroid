using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlHud : MonoBehaviour
{
    public TextMeshProUGUI tiempoTxt;
    public TextMeshProUGUI objetosTxt;
    public TextMeshProUGUI vidasTxt;

    public void SetVidasTXT(int vidas)
    {
        vidasTxt.text = "Vidas: " + vidas;
    }

    public void SetTiempoTXT(int tiempo)
    {
        int segundos = tiempo % 60;
        int minutos = tiempo / 60;
        tiempoTxt.text = minutos.ToString("00")+":"+segundos.ToString("00");
    }

    public void SetObjetosTXT(int cantidad)
    {
        objetosTxt.text = "Objetos: " + cantidad.ToString();
    }
}
