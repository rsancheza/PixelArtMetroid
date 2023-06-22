using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerJugador : MonoBehaviour
{
    MapeadoJugador mapeadoJugador;

    private void Awake()
    {
        mapeadoJugador = new MapeadoJugador();   
    }

    private void OnEnable()
    {
        mapeadoJugador.Jugador.Enable();

        mapeadoJugador.Jugador.Caminar.performed += MoverJugador;
        mapeadoJugador.Jugador.Caminar.canceled += MoverJugador;

        mapeadoJugador.Jugador.Saltar.started += SaltarJugador;

        mapeadoJugador.Jugador.Agachar.performed += AgacharJugador;
        mapeadoJugador.Jugador.Agachar.canceled += AgacharJugador;

        mapeadoJugador.Jugador.Disparar.started += JugadorDispara;
    }

    private void SaltarJugador(InputAction.CallbackContext obj)
    {
        FindObjectOfType<ControlJugador>().Saltar();
    }

    private void MoverJugador(InputAction.CallbackContext obj)
    {
        Vector2 vector2 = obj.ReadValue<Vector2>();
        FindObjectOfType<ControlJugador>().Correr(vector2);
    }

    private void AgacharJugador(InputAction.CallbackContext obj)
    {
        FindObjectOfType<ControlJugador>().Agachar();
    }

    private void JugadorDispara(InputAction.CallbackContext obj)
    {
        FindObjectOfType<ControlJugador>().Disparar();
    }

}
