using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControlJugador : MonoBehaviour
{
    public int velocidad = 4;
    public int fuerzaSalto = 0;
    public int puntuacion;
    public int numVidas;
    public int tiempoNivel;
    public Canvas canvas;
    public AudioClip saltosFX, vidasFX;
    public Transform controlDisparo;
    public GameObject bala;
    public bool puedeDisparar = true;

    private Rigidbody2D fisica;
    private SpriteRenderer sprite;
    private SpriteRenderer spriteAgachado;
    private Animator animacion;
    private bool vulnerable;
    private ControlHud hud;
    private int tiempoEmpleado;
    private ControlDatosJuego controlDatos;
    private float tiempoInicio;
    private Vector2 vector2;
    private bool pierdeVida = false;
    private AudioSource audioSource;
    private bool agachado = false;
    private bool mirandoDerecha = true;
    private bool disparando = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animacion = GetComponent<Animator>();
        vulnerable = true;
        hud = canvas.GetComponent<ControlHud>();
        tiempoInicio = Time.time;
        controlDatos = GameObject.Find("DatosJuego").GetComponent<ControlDatosJuego>();
    }
    
    public void FixedUpdate()
    {
        float entradaX = Input.GetAxis("Horizontal");
        fisica.velocity = new Vector2(entradaX * velocidad, fisica.velocity.y);
    }

    void Update()
    {

        if (fisica.velocity.x > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if(fisica.velocity.x < 0 && mirandoDerecha)
        {
            Girar();
        }


        AnimarJugador();

        hud.SetObjetosTXT(GameObject.FindGameObjectsWithTag("PowerUp").Length);

        //Comprobamos que no quedan powerUp y en tal caso finalizamos el juego como ganado.
        if(GameObject.FindGameObjectsWithTag("PowerUp").Length == 0)
        {
            GanarJuego();
        }

        tiempoEmpleado = (int)(Time.time - tiempoInicio);
        hud.SetTiempoTXT(tiempoNivel - tiempoEmpleado);

        if (tiempoNivel - tiempoEmpleado < 0)
        {
            FinJuego();
        }

        hud.SetVidasTXT(numVidas);

    }

    public void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        sprite.flipX = !sprite.flipX;
        transform.GetChild(1).eulerAngles = new Vector3(0, transform.GetChild(1).eulerAngles.y + 180, 0);
    }

    public void Disparar()
    {
        if (puedeDisparar)
        {
            Instantiate(bala, controlDisparo.position, controlDisparo.rotation);
            disparando = true;
            Invoke("NoDisparar", 1f);
        }
    }

    public void NoDisparar()
    {
        disparando = false;
    }

    public bool ControlVida()
    {
        pierdeVida = true;

        return pierdeVida;
    }

    public void QuitarVida()
    {
        if (vulnerable)
        {
            vulnerable = false;
            numVidas--;
            if (numVidas == 0)
            {
                FinJuego();
            }
            Invoke("HacerVulnerable",1f);
            audioSource.PlayOneShot(vidasFX);
        }
    }

    private void Invoke(string v)
    {
        throw new NotImplementedException();
    }

    public void HacerVulnerable()
    {
        vulnerable = true;
        sprite.color = Color.white;
    }

    public void FinJuego()
    {
        controlDatos.Ganador = false;
        SceneManager.LoadScene("FinDelJuego");
    }

    public void GanarJuego()
    {
        puntuacion += (numVidas * 100) + (tiempoNivel-tiempoEmpleado);
        controlDatos.Puntuacion = puntuacion;
        controlDatos.Ganador = true;
        SceneManager.LoadScene("FinDelJuego");
        
    }

    public void Correr(Vector2 context)
    {
        vector2 = context;
    }

    public void Saltar()
    {
        if (TocarSuelo())
        {
            fisica.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
            audioSource.PlayOneShot(saltosFX);
        }
    }

    public bool Agachar()
    {
        if(!agachado)
            agachado = true;
        else
            agachado = false;

        return agachado;
    }
    
    private bool TocarSuelo()
    {
        RaycastHit2D tocaSuelo = Physics2D.Raycast(transform.position+new Vector3(0,-2f,0),Vector2.down,0.2f);

        return tocaSuelo.collider != null;
        
    }

    private void AnimarJugador()
    {
        if (!vulnerable)
        {
            animacion.Play("JugadorHerido");
            puedeDisparar = false;
        }
        else 
        if (!TocarSuelo())
        {
            animacion.Play("JugadorSaltando");
            puedeDisparar = true;
        }
        else 
        if ((fisica.velocity.x > 1 || fisica.velocity.x < -1) && fisica.velocity.y == 0)
        {
            animacion.Play("JugadorCorriendo");
            puedeDisparar = true;
        }
        else 
        if (disparando)
        {
            animacion.Play("JugadorDispara");
            puedeDisparar = true;
        }
        else
        if (agachado)
        {
            animacion.Play("JugadorAgachado");
            puedeDisparar = false;
        }
        else 
        if ((fisica.velocity.x < 1 || fisica.velocity.x > -1) && fisica.velocity.y == 0)
        {
            animacion.Play("JugadorParado");
            puedeDisparar = false;
        }
    }

    public void IncrementarPuntos(int cantidad)
    {
        puntuacion+=cantidad;
    }

    public void IncrementarVelocidad()
    {
        velocidad = 10;
        Invoke("BajarVelocidad", 12f);
    }

    public void BajarVelocidad()
    {
        velocidad = 5;
    }

}
