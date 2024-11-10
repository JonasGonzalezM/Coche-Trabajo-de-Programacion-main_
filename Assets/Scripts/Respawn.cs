using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector3 spawnPosition; // Para almacenar la �ltima posici�n de spawn
    private Vector3 spawnRotation; // Para almacenar la �ltima rotaci�n del jugador
    private List<Vector3> checkpointPositions = new List<Vector3>(); // Lista para almacenar todas las posiciones de los checkpoints
    [SerializeField] private GameObject spawnpoint; // Esto te sirve para asignar el primer checkpoint en el inspector si lo necesitas

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position; // Inicializamos la posici�n con la posici�n actual del jugador
        spawnRotation = transform.eulerAngles; // Inicializamos la rotaci�n con la rotaci�n actual
    }

    // Update is called once per frame
    void Update()
    {
        // Si presionamos 'R', respawnea el jugador en la �ltima posici�n guardada
        if (Input.GetKeyDown(KeyCode.R))
        {
            RespawnPlayer();
        }
    }

    // Funci�n para hacer respawnear al jugador
    private void RespawnPlayer()
    {
        if (checkpointPositions.Count > 0)
        {
            // Respawnea al jugador en la �ltima posici�n registrada
            transform.position = checkpointPositions[checkpointPositions.Count - 1];
            transform.eulerAngles = spawnRotation; // Mantiene la �ltima rotaci�n
        }
        else
        {
            // Si no hay checkpoints, respawnea en la posici�n inicial
            transform.position = spawnPosition;
            transform.eulerAngles = spawnRotation;
        }
    }

    // Cuando el jugador entra en un checkpoint
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            // Guarda la posici�n del checkpoint actual en la lista
            checkpointPositions.Add(other.transform.position); // Agrega la nueva posici�n del checkpoint a la lista

            // Actualiza la �ltima posici�n y rotaci�n del jugador
            spawnPosition = other.transform.position;
            spawnRotation = transform.eulerAngles;
        }
    }
}
