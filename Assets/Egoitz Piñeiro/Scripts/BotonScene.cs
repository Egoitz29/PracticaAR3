using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonScene : MonoBehaviour

{
    public void CargarEscenaJuego()
    {
        SceneManager.LoadScene("Juego");
    }
}

