using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;  // Necesario para trabajar con Cinemachine.

public class CambioCamara : MonoBehaviour
{
    // Referencia a la cámara normal de Unity
    public Camera camaraNormal;

    // Referencia a la cámara virtual de Cinemachine
    public CinemachineVirtualCamera camaraVirtual;

    // Start se llama antes de que empiece el primer frame
    void Start()
    {
        // Inicialmente activamos la cámara normal
        ActivarCamara(0);
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Cambiar cámara cuando el jugador presiona la tecla "V"
        if (Input.GetKeyDown(KeyCode.V))
        {
            CambiarCamara();
        }
    }

    // Activamos una de las cámaras según el parámetro camaraActiva
    void ActivarCamara(int camaraActiva)
    {
        if (camaraActiva == 0)
        {
            // Activamos la cámara normal de Unity
            camaraNormal.gameObject.SetActive(true);

            // Desactivamos la cámara virtual
            camaraVirtual.gameObject.SetActive(false);
        }
        else if (camaraActiva == 1)
        {
            // Activamos la cámara virtual de Cinemachine
            camaraNormal.gameObject.SetActive(false);
            camaraVirtual.gameObject.SetActive(true);
        }
    }

    // Función para cambiar entre las cámaras cuando el jugador presiona la tecla "V"
    void CambiarCamara()
    {
        // Comprobamos si la cámara normal está activa
        if (camaraNormal.gameObject.activeSelf)
        {
            // Si la cámara normal está activa, cambiamos a la cámara virtual
            ActivarCamara(1);
        }
        else
        {
            // Si la cámara virtual está activa, cambiamos a la cámara normal
            ActivarCamara(0);
        }
    }
}
