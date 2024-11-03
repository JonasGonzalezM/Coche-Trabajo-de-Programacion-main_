using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rodillo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] Vector3 velocidadVector;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        //transform.eulerAngles = new Vector3(45, 45, 45);
        GetComponent<Rigidbody>().AddTorque(velocidadVector * velocidad, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddTorque(velocidadVector * velocidad * Time.deltaTime, Space.World);
        ////Las variabes 
    }
}
