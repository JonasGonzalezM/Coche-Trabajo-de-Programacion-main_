using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    [SerializeField] Canvas pausa;
    bool estaPausado;
    void Start()
    {
        pausa.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pausar();
        }
    }
    private void Pausar()
    {
        estaPausado = !estaPausado;
        pausa.gameObject.SetActive(estaPausado);

        if (estaPausado == true)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void Jugar()
    {
        pausa.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void Salir()
    {
        SceneManager.LoadScene(0);
    }

}
