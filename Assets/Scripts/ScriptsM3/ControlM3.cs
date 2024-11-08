using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ControlM3 : MonoBehaviour
{
    [System.Serializable]



    public class infoEje
    {
        public WheelCollider ruedaIzquierda;
        public WheelCollider ruedaDerecha;
        public bool motor;
        public bool direccion;
        

    }


    public List<infoEje> infoEjes;
    public float maxTorsionMotor;
    public float maxAnguloDeGiro;
    private int puntos;
    [SerializeField] TMP_Text textoPuntos;
    void posRuedas(WheelCollider collider)
    {
        if (collider.transform.childCount==0)
        {
            return;
        }
        Transform ruedaVisual = collider.transform.GetChild(0);
        Vector3 posicion;
        Quaternion rotacion;
        
        collider.GetWorldPose(out posicion, out rotacion);


        ruedaVisual.transform.position = posicion;
        ruedaVisual.transform.rotation = rotacion;

    }

    //private void Update()
    //{
        
    //}

    private void FixedUpdate()
    {
        

        float motor = maxTorsionMotor * Input.GetAxis("Vertical");
        float direccion = maxAnguloDeGiro * Input.GetAxis("Horizontal");

        foreach(infoEje ejesInfo in infoEjes)
        {
            if (ejesInfo.direccion)
            {
                ejesInfo.ruedaIzquierda.steerAngle = direccion;
                ejesInfo.ruedaDerecha.steerAngle = direccion;
            }


            if (ejesInfo.motor)
            {
                ejesInfo.ruedaIzquierda.motorTorque = motor;
                ejesInfo.ruedaDerecha.motorTorque = motor;
            }


            posRuedas(ejesInfo.ruedaIzquierda);
            posRuedas(ejesInfo.ruedaDerecha);
        }



    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coleccionable"))
        {
            Destroy(other.gameObject);
            puntos++;
            textoPuntos.text = "x " + puntos;
        }



    }

    //APARTADO DEL CRONOMETRO
    [SerializeField] TMP_Text cronometro;
    private float segundos;
    private int minutos;
    private float decimas;


    private void Update()
    {
        segundos += Time.deltaTime;
        if (segundos >= 60)
        {
            decimas = (segundos*60)/10;
            segundos = 0;
            minutos++;
        }
        cronometro.text= minutos.ToString("00")+" : "+ Mathf.Floor(segundos).ToString("00");
        //el ToString es para convertir los numeros a Texto y poder mostrarlos y el .Floor para redondear dichos numeros

        //decimas=(segundos*60)/10;
        // if(decimas>=100)
        //{
        // segundos++;
        //decimas=0;
    }
}
