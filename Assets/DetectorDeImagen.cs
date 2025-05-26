using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DetectorDeImagen : MonoBehaviour
{
    private ARTrackedImageManager manager;
    public ControladorFlechas guia;

    void Awake()
    {
        manager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        manager.trackedImagesChanged += ImagenDetectada;
    }

    void OnDisable()
    {
        manager.trackedImagesChanged -= ImagenDetectada;
    }

    void ImagenDetectada(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var imagen in args.added)
        {
            if (imagen.referenceImage.name == "PuertaAula")
            {
                guia.InstanciarFlechas(imagen);
            }
        }
    }
}
