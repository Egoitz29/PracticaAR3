using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class InstanciadorPrefabs : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public TMP_Text textoPlanosHorizontales;
    public TMP_Text textoPlanosVerticales;
    public TMP_Text textoPrefabsDeseados;
    public TMP_Text textoResumenFinal;
    public TMP_Text textoTiempoConfigurado;
    public TMP_Text textoTemporizadorEnCurso; // NUEVO
    public GameObject prefabAInstanciar;
    public Button botonCrear;

    private int cantidadHorizontales;
    private int cantidadVerticales;

    private List<ARPlane> planosHorizontales = new List<ARPlane>();
    private List<ARPlane> planosVerticales = new List<ARPlane>();

    private int totalInstanciados = 0;
    private int totalRestantes = 0;
    private bool botonMostrado = false;
    private bool yaInstanciado = false;

    private float tiempoRestante = 0f;           // NUEVO
    private bool temporizadorActivo = false;     // NUEVO

    void Start()
    {
        cantidadHorizontales = PlayerPrefs.GetInt("CantidadHorizontales", 1);
        cantidadVerticales = PlayerPrefs.GetInt("CantidadVerticales", 1);
        int tiempoConfigurado = PlayerPrefs.GetInt("TiempoPartida", 30);

        if (textoTiempoConfigurado != null)
            textoTiempoConfigurado.text = $"Tiempo configurado: {tiempoConfigurado} segundos";

        if (textoTemporizadorEnCurso != null)
            textoTemporizadorEnCurso.gameObject.SetActive(false); // Ocultar hasta que se inicie

        botonCrear.gameObject.SetActive(false);
        botonCrear.onClick.AddListener(InstanciarEnPlanosAleatoriamente);

        if (textoPrefabsDeseados != null)
            textoPrefabsDeseados.text = $"Prefabs elegidos: Horizontales: {cantidadHorizontales} | Verticales: {cantidadVerticales}";

        if (textoResumenFinal != null)
            textoResumenFinal.gameObject.SetActive(false);
    }

    void Update()
    {
        if (planeManager == null) return;

        planosHorizontales.Clear();
        planosVerticales.Clear();

        foreach (var plano in planeManager.trackables)
        {
            if (plano.alignment == PlaneAlignment.HorizontalUp || plano.alignment == PlaneAlignment.HorizontalDown)
                planosHorizontales.Add(plano);
            else if (plano.alignment == PlaneAlignment.Vertical)
                planosVerticales.Add(plano);
        }

        if (textoPlanosHorizontales != null)
            textoPlanosHorizontales.text = $"Detectados horizontales: {planosHorizontales.Count}";

        if (textoPlanosVerticales != null)
            textoPlanosVerticales.text = $"Detectados verticales: {planosVerticales.Count}";

        if (!botonMostrado && planosHorizontales.Count >= cantidadHorizontales && planosVerticales.Count >= cantidadVerticales)
        {
            botonCrear.gameObject.SetActive(true);
            botonMostrado = true;
        }

        // TEMPORIZADOR EN CURSO
        if (temporizadorActivo && tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;

            if (textoTemporizadorEnCurso != null)
                textoTemporizadorEnCurso.text = $"Tiempo restante: {Mathf.CeilToInt(tiempoRestante)}s";

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                temporizadorActivo = false;

                if (textoTemporizadorEnCurso != null)
                    textoTemporizadorEnCurso.text = "¡Tiempo agotado!";

                // Aquí puedes hacer más acciones: destruir prefabs, mostrar canvas final, etc.
            }
        }
    }

    void InstanciarEnPlanosAleatoriamente()
    {
        if (yaInstanciado) return;

        totalInstanciados = 0;
        totalRestantes = 0;

        for (int i = 0; i < cantidadHorizontales; i++)
        {
            ARPlane plano = planosHorizontales[Random.Range(0, planosHorizontales.Count)];
            Vector3 posicion = plano.center + plano.transform.position;
            Instantiate(prefabAInstanciar, posicion, Quaternion.identity);
            totalInstanciados++;
        }

        for (int i = 0; i < cantidadVerticales; i++)
        {
            ARPlane plano = planosVerticales[Random.Range(0, planosVerticales.Count)];
            Vector3 posicion = plano.center + plano.transform.position;
            Instantiate(prefabAInstanciar, posicion, Quaternion.identity);
            totalInstanciados++;
        }

        totalRestantes = totalInstanciados;
        yaInstanciado = true;
        botonCrear.gameObject.SetActive(false);

        if (textoPlanosHorizontales != null) textoPlanosHorizontales.gameObject.SetActive(false);
        if (textoPlanosVerticales != null) textoPlanosVerticales.gameObject.SetActive(false);
        if (textoPrefabsDeseados != null) textoPrefabsDeseados.gameObject.SetActive(false);
        if (textoTiempoConfigurado != null) textoTiempoConfigurado.gameObject.SetActive(false);

        if (textoResumenFinal != null)
        {
            textoResumenFinal.gameObject.SetActive(true);
            textoResumenFinal.text = $"Prefabs restantes: {totalRestantes}";
        }

        // INICIAR TEMPORIZADOR
        tiempoRestante = PlayerPrefs.GetInt("TiempoPartida", 30);
        temporizadorActivo = true;

        if (textoTemporizadorEnCurso != null)
        {
            textoTemporizadorEnCurso.gameObject.SetActive(true);
            textoTemporizadorEnCurso.text = $"Tiempo restante: {Mathf.CeilToInt(tiempoRestante)}s";
        }
    }

    public void ReportarRecoleccion()
    {
        totalRestantes--;

        if (textoResumenFinal != null)
        {
            if (totalRestantes > 0)
                textoResumenFinal.text = $"Prefabs restantes: {totalRestantes}";
            else
                textoResumenFinal.text = "¡Has recogido todos!";
        }
    }
}
