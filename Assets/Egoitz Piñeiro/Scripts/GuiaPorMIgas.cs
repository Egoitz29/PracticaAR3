using UnityEngine;
using System.Collections.Generic;

public class GuiaPorMigas : MonoBehaviour
{
    public GameObject migaPrefab;
    public Vector3[] posicionesRelativas;

    [Header("Referencia colocada manualmente")]
    public Transform origenManual;

    private List<GameObject> migasInstanciadas = new List<GameObject>();

    void Start()
    {
        if (origenManual == null)
        {
            Debug.LogError("Debes arrastrar el objeto de referencia manual en el Inspector.");
            return;
        }

        InstanciarTodasLasMigas();
    }

    void InstanciarTodasLasMigas()
    {
        for (int i = 0; i < posicionesRelativas.Length; i++)
        {
            Vector3 posicionMundo = origenManual.TransformPoint(posicionesRelativas[i]);
            GameObject nuevaMiga = Instantiate(migaPrefab, posicionMundo, origenManual.rotation);
            nuevaMiga.SetActive(false);
            nuevaMiga.GetComponent<MigaSecuencial>().AsignarSiguienteMiga(null);

            migasInstanciadas.Add(nuevaMiga);
        }

        // Enlaza cada miga con la siguiente
        for (int i = 0; i < migasInstanciadas.Count - 1; i++)
        {
            migasInstanciadas[i].GetComponent<MigaSecuencial>().AsignarSiguienteMiga(migasInstanciadas[i + 1]);
        }

        // Activa solo la primera
        if (migasInstanciadas.Count > 0)
            migasInstanciadas[0].SetActive(true);
    }
}
