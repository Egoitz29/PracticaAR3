using UnityEngine;

public class MIga : MonoBehaviour
{
    private GameObject siguienteMiga;

    public void AsignarSiguienteMiga(GameObject siguiente)
    {
        siguienteMiga = siguiente;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera") || other.CompareTag("Player"))
        {
            if (siguienteMiga != null)
                siguienteMiga.SetActive(true);

            Destroy(gameObject); // Destruye la miga actual
        }
    }
}
