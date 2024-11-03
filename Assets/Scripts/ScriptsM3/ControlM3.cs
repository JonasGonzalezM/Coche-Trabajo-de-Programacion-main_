using System.Collections;
using System.Collections.Generic;
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



    private void FixedUpdate()
    {
        Coleccionable();

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


    void Coleccionable()
    {











    }
}
