using UnityEngine;

public class GuiaPorMigas : MonoBehaviour
{
    public GameObject migaPrefab; // Solo uno, siempre el mismo
    public Vector3[] posicionesRelativas;

    [Header("Solo para simulación en Editor")]
    public Vector3 posicionSimulada = Vector3.zero;
    public Quaternion rotacionSimulada = Quaternion.identity;

    private int indiceActual = 0;
    private Transform origen;

    void Start()
    {
        GameObject imagenFakeGO = GameObject.Find("FakeImage");
        if (imagenFakeGO == null)
        {
            imagenFakeGO = new GameObject("FakeImage");
        }

        imagenFakeGO.transform.position = posicionSimulada;
        imagenFakeGO.transform.rotation = rotacionSimulada;

        origen = imagenFakeGO.transform;
        InstanciarSiguienteMiga();
    }

    public void InstanciarSiguienteMiga()
    {
        if (indiceActual >= posicionesRelativas.Length)
        {
            Debug.Log("Todas las migas han sido colocadas.");
            return;
        }

        Vector3 posicionMundo = origen.TransformPoint(posicionesRelativas[indiceActual]);
        GameObject nuevaMiga = Instantiate(migaPrefab, posicionMundo, rotacionSimulada);
        indiceActual++;
    }
}
