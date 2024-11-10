using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody rb;
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
        if (collider.transform.childCount == 0)
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

        foreach (infoEje ejesInfo in infoEjes) // El in en el foreach es utilizado para especificar de qué colección o conjunto de elementos deseo iterar.
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
    //APARTADO DE TRIGGERS

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Coleccionable"))
        {
            Destroy(other.gameObject);
            puntos++;
            textoPuntos.text = "x " + puntos;
        }

        if (other.gameObject.CompareTag("Final"))
        {
            PlayerPrefs.SetInt("Puntuacionm", minutos);
            PlayerPrefs.SetFloat("Puntuacions", Mathf.Floor(segundos));
            SceneManager.LoadScene("Puntuaciones");

        }

        if (other.gameObject.CompareTag("Nitro"))
        {

        }

        //if (other.tag == "Nitro")
        //{

        //}




    }

    //APARTADO DEL CRONOMETRO
    [SerializeField] TMP_Text cronometro;
    private float segundos;
    private int minutos;
    //private float decimas;


    private void Update()
    {
        segundos += Time.deltaTime;
        if (segundos >= 60)
        {

            //decimas = (segundos*60)/10;
            segundos = 0;
            minutos++;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            segundos = 0;
            minutos = 0;
        }
        cronometro.text = minutos.ToString("00") + " : " + Mathf.Floor(segundos).ToString("00");

        //el ToString es para convertir los numeros a Texto y poder mostrarlos y el .Floor para redondear dichos numeros

        //decimas=(segundos*60)/10;
        // if(decimas>=100)
        //{
        // segundos++;
        //decimas=0;





    }
}