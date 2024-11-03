using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private float velocidad = 3f;
    [SerializeField] Vector3 direccionMovimiento= new Vector3(0,1,0);
    [SerializeField] Vector3 rotacionColeccion = new Vector3(0,1,0);

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3 (0,30,0)*Time.deltaTime);


        timer += Time.deltaTime;
        transform.Translate(direccionMovimiento * velocidad * Time.deltaTime);


        if (timer >= 0.5f)
        {
            timer = 0;
            direccionMovimiento *= -1;

        }



    }



    
}