using UnityEngine.XR.ARFoundation;
using UnityEngine;

public class SimuladorImagen : MonoBehaviour
{
    public ControladorFlechas guia;

    void Start()
    {
        ARTrackedImage falsaImagen = new GameObject("FalsaImagen").AddComponent<ARTrackedImage>();
        falsaImagen.transform.position = transform.position;
        falsaImagen.transform.rotation = Quaternion.identity;

        guia.InstanciarFlechas(falsaImagen);
    }
}
