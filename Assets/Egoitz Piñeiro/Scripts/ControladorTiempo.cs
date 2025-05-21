using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControladorTiempo : MonoBehaviour
{
    [Header("Textos de visualización")]
    public TMP_Text tiempoTexto;
    public TMP_Text textoHorizontales;
    public TMP_Text textoVerticales;

    [Header("Valores de configuración")]
    private int tiempo = 30;
    private int cantidadHorizontales = 1;
    private int cantidadVerticales = 1;

    private const int tiempoMin = 5;
    private const int tiempoMax = 300;
    private const int prefabMin = 0;
    private const int prefabMax = 10;

    void Start()
    {
        ActualizarTodosLosTextos();
    }

    // Tiempo
    public void IncrementarTiempo()
    {
        if (tiempo < tiempoMax)
        {
            tiempo += 1;
            ActualizarTodosLosTextos();
        }
    }

    public void DisminuirTiempo()
    {
        if (tiempo > tiempoMin)
        {
            tiempo -= 1;
            ActualizarTodosLosTextos();
        }
    }

    // Horizontales
    public void IncrementarHorizontales()
    {
        if (cantidadHorizontales < prefabMax)
        {
            cantidadHorizontales++;
            ActualizarTodosLosTextos();
        }
    }

    public void DisminuirHorizontales()
    {
        if (cantidadHorizontales > prefabMin)
        {
            cantidadHorizontales--;
            ActualizarTodosLosTextos();
        }
    }

    // Verticales
    public void IncrementarVerticales()
    {
        if (cantidadVerticales < prefabMax)
        {
            cantidadVerticales++;
            ActualizarTodosLosTextos();
        }
    }

    public void DisminuirVerticales()
    {
        if (cantidadVerticales > prefabMin)
        {
            cantidadVerticales--;
            ActualizarTodosLosTextos();
        }
    }

    void ActualizarTodosLosTextos()
    {
        if (tiempoTexto != null)
            tiempoTexto.text = "Duración: " + tiempo + " segundos";

        if (textoHorizontales != null)
            textoHorizontales.text = "Horizontales: " + cantidadHorizontales;

        if (textoVerticales != null)
            textoVerticales.text = "Verticales: " + cantidadVerticales;
    }

    public void GuardarTodoYContinuar()
    {
        PlayerPrefs.SetInt("TiempoPartida", tiempo);
        PlayerPrefs.SetInt("CantidadHorizontales", cantidadHorizontales);
        PlayerPrefs.SetInt("CantidadVerticales", cantidadVerticales);
        PlayerPrefs.Save();

        Debug.Log($"Tiempo: {tiempo}s, Horizontales: {cantidadHorizontales}, Verticales: {cantidadVerticales}");

        SceneManager.LoadScene("Juego"); // Cambia por el nombre real de tu escena
    }
}
