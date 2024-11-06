using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Velocimetro : MonoBehaviour
{
    [SerializeField] public Rigidbody target;
    [SerializeField] public float maxSpeed = 0.0f; //"velocidad máxima del target en KM/H
    [SerializeField] public float anguloVelocidadMinima;
    [SerializeField] public float anguloVelocidadMaxima;

    [Header("UI")]
    public TextMeshProUGUI panelVelocidad; // El Texto que muestra la velocidad
    public RectTransform aguja; // esto es para el giro de la aguja del velocimetro

    private float speed = 0.0f;


    private void Update()
    {
        // 3.6 para convertirlo a KM/H
        // La velocidad debe de ser limitada en el controlador del coche
        speed = target.velocity.magnitude * 3.6f;

        if(panelVelocidad != null)
        {
            panelVelocidad.text = ((int)speed) + " KM/H";
        }

        if(aguja != null)
        {
            aguja.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(anguloVelocidadMinima, anguloVelocidadMaxima, speed / maxSpeed));
        }
    }
}
