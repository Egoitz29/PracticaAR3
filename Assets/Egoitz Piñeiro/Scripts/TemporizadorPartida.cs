using UnityEngine;
using TMPro;

public class TemporizadorPartida : MonoBehaviour
{
    public TMP_Text tiempoTexto;
    private float tiempoRestante;

    void Start()
    {
        tiempoRestante = PlayerPrefs.GetInt("TiempoPartida", 30);  // ← Lee lo que se guardó antes
        ActualizarTexto();
    }

    void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarTexto();
        }
        else if (tiempoRestante <= 0 && tiempoTexto.text != "¡Tiempo agotado!")
        {
            tiempoTexto.text = "¡Tiempo agotado!";
            // Aquí puedes activar canvas final, mostrar botón, etc.
        }
    }

    void ActualizarTexto()
    {
        tiempoTexto.text = "Tiempo restante: " + Mathf.CeilToInt(tiempoRestante) + "s";
    }
}
