using UnityEngine;

public class PrefabRecolectable : MonoBehaviour
{
    public float distanciaDestruccion = 0.5f;
    private Transform jugador;
    private bool destruido = false;
    public AudioClip sonidoRecoleccion; // NUEVO
    private AudioSource audioSource;    // NUEVO

    void Start()
    {
        jugador = Camera.main.transform;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!destruido && Vector3.Distance(transform.position, jugador.position) < distanciaDestruccion)
        {
            destruido = true;

            // Reproducir el sonido
            if (audioSource != null && sonidoRecoleccion != null)
                audioSource.PlayOneShot(sonidoRecoleccion);

            // Llamar al gestor
            InstanciadorPrefabs gestor = FindObjectOfType<InstanciadorPrefabs>();
            if (gestor != null)
                gestor.ReportarRecoleccion();

            // Destruir después de un pequeño delay (esperar a que suene)
            Destroy(gameObject, 0.2f); // da tiempo a que suene el audio
        }
    }
}
