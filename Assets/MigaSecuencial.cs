using UnityEngine;

public class MigaSecuencial : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            GuiaPorMigas guia = FindObjectOfType<GuiaPorMigas>();
            if (guia != null)
            {
                guia.InstanciarSiguienteMiga();
            }

            Destroy(gameObject);
        }
    }
}
