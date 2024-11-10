using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitro : MonoBehaviour
{
    public Vector3 impulso = new Vector3(0, 0, 90); // Dirección y fuerza del impulso

    private void OnTriggerEnter()
    {
        // Verifica si el objeto que colisiona tiene el tag "Player" o el nombre del objeto que usas para el jugador
        if (this.gameObject.CompareTag("Player"))
        {
            // Obtiene el Rigidbody del jugador (asume que el jugador tiene un Rigidbody)
            Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Aplica el impulso
                rb.AddForce(impulso, ForceMode.Impulse);
            }
        }
    }
    
}
