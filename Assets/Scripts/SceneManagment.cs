using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour
{
    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);




    }

   public void Juego()
   {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
   }


    public void Salir()
    {
        Application.Quit();
    }
    
}
