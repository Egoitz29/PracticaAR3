using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ControladorFlechas : MonoBehaviour

{
    [Header("Prefabs de flechas (puede ser el mismo repetido)")]
    public GameObject[] flechasPrefabs;

    [Header("Posiciones relativas en metros respecto al patrón")]
    public Vector3[] posicionesRelativas;

    private GameObject[] flechasInstanciadas;
    private int flechaActiva = 0;
    private bool instanciadas = false;

    [Header("Distancia mínima para activar la siguiente flecha")]
    public float distanciaParaActivarSiguiente = 0.6f;

    void Update()
    {
        if (!instanciadas || flechaActiva >= flechasInstanciadas.Length)
            return;

        float distancia = Vector3.Distance(Camera.main.transform.position, flechasInstanciadas[flechaActiva].transform.position);

        if (distancia < distanciaParaActivarSiguiente)
        {
            flechasInstanciadas[flechaActiva].SetActive(false);
            flechaActiva++;

            if (flechaActiva < flechasInstanciadas.Length)
            {
                flechasInstanciadas[flechaActiva].SetActive(true);
            }
        }
    }

    public void InstanciarFlechas(ARTrackedImage trackedImage)
    {
        if (instanciadas) return;

        flechasInstanciadas = new GameObject[flechasPrefabs.Length];

        for (int i = 0; i < flechasPrefabs.Length; i++)
        {
            Vector3 posicionMundo = trackedImage.transform.TransformPoint(posicionesRelativas[i]);
            Quaternion rotacion = trackedImage.transform.rotation;

            GameObject flecha = Instantiate(flechasPrefabs[i], posicionMundo, rotacion);
            flecha.SetActive(i == 0); // Solo la primera flecha se activa

            flechasInstanciadas[i] = flecha;
        }

        instanciadas = true;
    }
}

