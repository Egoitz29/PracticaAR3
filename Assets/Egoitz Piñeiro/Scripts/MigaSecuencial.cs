using UnityEngine;

public class MigaSecuencial : MonoBehaviour
{
    private GameObject siguienteMiga;

    public void AsignarSiguienteMiga(GameObject siguiente)
    {
        siguienteMiga = siguiente;
    }
    void Update()
    {
        GameObject detector = GameObject.FindWithTag("Detector");
        if (detector != null && Vector3.Distance(transform.position, detector.transform.position) < 0.2f)
        {
            Debug.Log("¡Está muy cerca del detector!");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger activado por: " + other.name);

        if (other.CompareTag("Detector")) // O el tag que hayas usado
        {
            if (siguienteMiga != null)
            {
                siguienteMiga.SetActive(true);
                Debug.Log("Activando siguiente miga: " + siguienteMiga.name);
            }

            Destroy(gameObject);
        }
    }

}
