using UnityEngine;

public class Hrac : MonoBehaviour
{
    public float rychlost = 10f;
    private CharacterController controller;

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        // Tohle teď bude fungovat díky nastavení "Both"
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 pohyb = transform.right * x + transform.forward * z;
        controller.SimpleMove(pohyb * rychlost);
    }
}