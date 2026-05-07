using UnityEngine;

public class Pickup : MonoBehaviour
{
    public void Interact()
    {
        Debug.Log("Předmět sebrán!");
        // Tady přidáš kód pro přidání do tvého inventáře
        Destroy(gameObject); // Pro teď předmět jen zmizí
    }
}