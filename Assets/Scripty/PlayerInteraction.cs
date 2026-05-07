using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float dosah = 3f; 
    public LayerMask vrstvaInterakce; // V Unity nastav na "Interactable"

    void Update()
    {
        // Paprsek ze středu kamery
        Ray ray = GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, dosah, vrstvaInterakce))
        {
            // Pokud míříme na něco se skriptem ItemBase
            ItemBase item = hit.collider.GetComponent<ItemBase>();
            if (item != null)
            {
                // Tady bys mohl změnit barvu crosshairu na červenou
                if (Input.GetKeyDown(KeyCode.E))
                {
                    item.Interact();
                }
            }
        }
    }
}