using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    Vector3 spawnPosition;
    Vector3 spawnRotation;
    [SerializeField] GameObject spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            transform.position = spawnPosition;
            transform.eulerAngles = spawnRotation;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Checkpoint"))
        {
            spawnPosition=spawnpoint.transform.position;
            spawnRotation=transform.eulerAngles;
        }
    }
}
