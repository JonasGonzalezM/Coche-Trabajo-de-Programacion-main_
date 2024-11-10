using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MostrarTiempo : MonoBehaviour
{
    [SerializeField] TMP_Text textoTiempo;
    private int minutos;
    private float segundos;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        minutos = PlayerPrefs.GetInt("Puntuacionm");
        segundos = PlayerPrefs.GetFloat("Puntuacions");
        textoTiempo.text= minutos.ToString("00")+" : "+Mathf.Floor(segundos).ToString("00");




    }
}
