using System.Collections;
using System.Collections.Generic;
using TMPro;  // Importamos el espacio de nombres para la UI de texto (si usas TextMesh Pro).
using UnityEngine;  // Espacio de nombres principal para interactuar con Unity.
using UnityEngine.SceneManagement;  // Para poder cargar otras escenas (como la de puntuaciones).

public class ControlM3 : MonoBehaviour
{
    [System.Serializable]
    public class InfoEje
    {
        public WheelCollider ruedaIzquierda;
        public WheelCollider ruedaDerecha;
        public bool motor;
        public bool direccion;
    }

    public List<InfoEje> infoEjes;
    public float maxTorsionMotor = 700f;
    public float maxAnguloDeGiro = 30f;
    private float nitroBoostMultiplier = 4f;
    private float tiempoDeNitroRestante;
    private int puntos;
    private float segundos;
    private int minutos;
    [SerializeField] TMP_Text textoPuntos;
    [SerializeField] TMP_Text cronometro;
    private float torqueOriginal;
    private bool nitroActivo = false;

    // Variables para el derrape
    private bool esDerrape = false;  // Determina si el coche está derrapando.
    private float factorDeDerrape = 0.2f;  // Factor de reducción de la fricción lateral durante el derrape.

    void Start()
    {
        puntos = 0;
        segundos = 0;
        minutos = 0;
        torqueOriginal = maxTorsionMotor;
        ActualizarTextoPuntos();  // Actualizamos los puntos al iniciar el juego.
    }

    void FixedUpdate()
    {
        // Obtenemos las entradas del jugador para controlar el coche
        float motorInput = Input.GetAxis("Vertical");  // Movimiento hacia adelante y atrás (W/S o flechas arriba/abajo).
        float direccionInput = Input.GetAxis("Horizontal");  // Movimiento de dirección (A/D o flechas izquierda/derecha).

        // Calculamos el valor del torque (fuerza) que se aplica al motor.
        float motor = maxTorsionMotor * motorInput;
        float direccion = maxAnguloDeGiro * direccionInput;

        // Si el nitro está activado, aumentamos el torque del motor y gestionamos la fricción.
        if (nitroActivo)
        {
            motor = torqueOriginal * nitroBoostMultiplier * motorInput;  // Aplicamos el aumento de torque solo si el nitro está activado.
            AumentarFriccionNitro();  // Aumentamos la fricción de las ruedas durante el nitro.
            esDerrape = false;  // Desactivamos el derrape cuando el nitro está activo.
        }
        else
        {
            // Si el nitro no está activado, permitimos el derrape si se presiona la tecla de espacio.
            if (Input.GetKey(KeyCode.Space))  // Si el jugador presiona la tecla de espacio, el coche derrapará.
            {
                esDerrape = true;
                ActivarDerrape();  // Activamos el derrape, reduciendo la fricción lateral de las ruedas.
            }
            else
            {
                esDerrape = false;
                RestablecerFriccionDerrape();  // Si no estamos derrapando, restauramos la fricción lateral normal.
            }
        }

        // Aplicamos los valores de torque y dirección a todas las ruedas del coche.
        foreach (InfoEje eje in infoEjes)
        {
            if (eje.direccion)
            {
                // Si el eje controla la dirección de las ruedas, aplicamos el ángulo de giro.
                eje.ruedaIzquierda.steerAngle = direccion;  // Dirección de la rueda izquierda.
                eje.ruedaDerecha.steerAngle = direccion;    // Dirección de la rueda derecha.
            }

            if (eje.motor)
            {
                // Si el eje controla el motor, aplicamos el torque calculado.
                eje.ruedaIzquierda.motorTorque = motor;  // Torque de la rueda izquierda.
                eje.ruedaDerecha.motorTorque = motor;    // Torque de la rueda derecha.
            }

            PosicionarRuedas(eje.ruedaIzquierda);  // Aseguramos que las ruedas visuales sigan el movimiento físico.
            PosicionarRuedas(eje.ruedaDerecha);
        }
    }

    void PosicionarRuedas(WheelCollider rueda)
    {
        // Verificamos si la rueda tiene un objeto hijo (el modelo visual de la rueda).
        if (rueda.transform.childCount == 0) return;

        // Obtenemos la posición y la rotación global de la rueda en el mundo.
        rueda.GetWorldPose(out Vector3 posicion, out Quaternion rotacion);
        Transform ruedaVisual = rueda.transform.GetChild(0);  // Accedemos al primer hijo (el modelo visual de la rueda).
        ruedaVisual.SetPositionAndRotation(posicion, rotacion);  // Establecemos la posición y rotación del modelo visual de la rueda.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coleccionable"))
        {
            // Si el objeto con el que colisionamos es un "Coleccionable", lo destruimos y aumentamos los puntos.
            Destroy(other.gameObject);
            puntos++;
            ActualizarTextoPuntos();
        }
        else if (other.CompareTag("Final"))
        {
            // Si el objeto es la meta ("Final"), guardamos el puntaje y cambiamos a la escena de puntuaciones.
            PlayerPrefs.SetInt("Puntuacionm", minutos);
            PlayerPrefs.SetFloat("Puntuacions", Mathf.Floor(segundos));
            SceneManager.LoadScene("Puntuaciones");
        }
        else if (other.CompareTag("Nitro"))
        {
            // Si el objeto es "Nitro", activamos el nitro por 2 segundos.
            ActivarNitro(2f);
        }
    }

    void Update()
    {
        // Actualizamos el cronómetro en cada frame.
        ActualizarCronometro();

        if (Input.GetKeyDown(KeyCode.R))
        {
            // Si se presiona la tecla "R", reiniciamos el cronómetro.
            ResetearCronometro();
        }

        // Si el nitro está activado, disminuimos el tiempo restante del nitro.
        if (nitroActivo)
        {
            tiempoDeNitroRestante -= Time.deltaTime;  // Restamos el tiempo en función del tiempo transcurrido entre frames.

            // Si el tiempo restante del nitro llega a 0, desactivamos el nitro y restauramos el torque original.
            if (tiempoDeNitroRestante <= 0f)
            {
                nitroActivo = false;
                maxTorsionMotor = torqueOriginal;
                RestablecerFriccion();  // Restauramos la fricción cuando el nitro se apaga.
            }
        }
    }

    void ActualizarTextoPuntos()
    {
        // Actualizamos el texto que muestra los puntos en la UI.
        textoPuntos.text = "x " + puntos;
    }

    void ActualizarCronometro()
    {
        // Actualizamos el cronómetro sumando el tiempo transcurrido en cada frame.
        segundos += Time.deltaTime;

        if (segundos >= 60)
        {
            segundos = 0;
            minutos++;
        }

        cronometro.text = $"{minutos:00} : {Mathf.Floor(segundos):00}";  // Mostramos el cronómetro en formato "mm:ss".
    }

    void ResetearCronometro()
    {
        // Reiniciamos el cronómetro.
        segundos = 0;
        minutos = 0;
    }

    void ActivarNitro(float duracion)
    {
        // Activamos el nitro durante una duración especificada.
        nitroActivo = true;
        tiempoDeNitroRestante = duracion;
        maxTorsionMotor = torqueOriginal * nitroBoostMultiplier;  // Aumentamos el torque al valor de nitro.
    }

    // Función que aumenta la fricción de las ruedas durante el nitro.
    void AumentarFriccionNitro()
    {
        foreach (InfoEje eje in infoEjes)
        {
            WheelFrictionCurve friccion = eje.ruedaIzquierda.forwardFriction;
            friccion.stiffness = 2f;  // Aumentamos la rigidez de la fricción para que las ruedas agarren mejor.
            eje.ruedaIzquierda.forwardFriction = friccion;
            eje.ruedaDerecha.forwardFriction = friccion;
        }
    }

    // Función que activa el derrape, reduciendo la fricción lateral.
    void ActivarDerrape()
    {
        if (!nitroActivo)  // Solo permitimos el derrape si el nitro no está activo.
        {
            foreach (InfoEje eje in infoEjes)
            {
                WheelFrictionCurve friccion = eje.ruedaIzquierda.sidewaysFriction;
                friccion.stiffness = factorDeDerrape;  // Reducimos la fricción lateral para permitir el derrape.
                eje.ruedaIzquierda.sidewaysFriction = friccion;
                eje.ruedaDerecha.sidewaysFriction = friccion;
            }
        }
    }

    // Restaura la fricción lateral a su valor normal después de un derrape.
    void RestablecerFriccionDerrape()
    {
        foreach (InfoEje eje in infoEjes)
        {
            WheelFrictionCurve friccion = eje.ruedaIzquierda.sidewaysFriction;
            friccion.stiffness = 1f;  // Restauramos la fricción lateral a su valor normal.
            eje.ruedaIzquierda.sidewaysFriction = friccion;
            eje.ruedaDerecha.sidewaysFriction = friccion;
        }
    }

    // Restauramos la fricción normal cuando el nitro no está activo.
    void RestablecerFriccion()
    {
        foreach (InfoEje eje in infoEjes)
        {
            WheelFrictionCurve friccion = eje.ruedaIzquierda.forwardFriction;
            friccion.stiffness = 1f;  // Restauramos la fricción a su valor original.
            eje.ruedaIzquierda.forwardFriction = friccion;
            eje.ruedaDerecha.forwardFriction = friccion;
        }
    }
}
