using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCoche : MonoBehaviour
{
    [SerializeField] Vector3 movimientoArriba = new Vector3(0,1,0);
    [SerializeField] Vector3 Giro= Vector3.zero;
    [SerializeField] private int fuerza = 1000;
    [SerializeField] private int contador = 5;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.W))
        //{
        //    movementInput.z = 3*Time.deltaTime;



        //} 
        //if (Input.GetKey(KeyCode.S))
        //{
        //    movementInput.z = -3 * Time.deltaTime;

        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    //movementInput.z = 3;
        //    rotationAngle.y = 45 * Time.deltaTime;

        //}

        //if (Input.GetKey(KeyCode.W))
        //{
        //    //movementInput.z = 3;
        //    rotationAngle.y = -45 * Time.deltaTime;

        //}

        VelocidadM3();
    }

    void VelocidadM3()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            contador--;
            if(contador>0)
            {
                rb.AddForce(movimientoArriba * fuerza, ForceMode.Impulse);
            }
            



        }
        contador = 5;
    }


}
