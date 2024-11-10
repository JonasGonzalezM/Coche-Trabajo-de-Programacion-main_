using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;  // Necesario para trabajar con Cinemachine.

public class CambioCamara : MonoBehaviour
{
    // Referencia a la c�mara normal de Unity
    public Camera camaraNormal;

    // Referencia a la c�mara virtual de Cinemachine
    public CinemachineVirtualCamera camaraVirtual;

    // Start se llama antes de que empiece el primer frame
    void Start()
    {
        // Inicialmente activamos la c�mara normal
        ActivarCamara(0);
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Cambiar c�mara cuando el jugador presiona la tecla "V"
        if (Input.GetKeyDown(KeyCode.V))
        {
            CambiarCamara();
        }
    }

    // Activamos una de las c�maras seg�n el par�metro camaraActiva
    void ActivarCamara(int camaraActiva)
    {
        if (camaraActiva == 0)
        {
            // Activamos la c�mara normal de Unity
            camaraNormal.gameObject.SetActive(true);

            // Desactivamos la c�mara virtual
            camaraVirtual.gameObject.SetActive(false);
        }
        else if (camaraActiva == 1)
        {
            // Activamos la c�mara virtual de Cinemachine
            camaraNormal.gameObject.SetActive(false);
            camaraVirtual.gameObject.SetActive(true);
        }
    }

    // Funci�n para cambiar entre las c�maras cuando el jugador presiona la tecla "V"
    void CambiarCamara()
    {
        // Comprobamos si la c�mara normal est� activa
        if (camaraNormal.gameObject.activeSelf)
        {
            // Si la c�mara normal est� activa, cambiamos a la c�mara virtual
            ActivarCamara(1);
        }
        else
        {
            // Si la c�mara virtual est� activa, cambiamos a la c�mara normal
            ActivarCamara(0);
        }
    }
}
