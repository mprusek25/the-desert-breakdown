using UnityEngine;

public class SimplePlayerMove : MonoBehaviour
{
    public float speed = 7f;
    public float rotationSpeed = 100f;
    private CharacterController controller;

    void Start()
    {
        // Najde Character Controller, který jsi přidal na postavu
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. OTÁČENÍ (A/D nebo Šipky do stran)
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        // 2. POHYB (W/S nebo Šipky nahoru/dolů)
        // Pohybujeme se ve směru, kam postava kouká (transform.forward)
        Vector3 move = transform.forward * Input.GetAxis("Vertical") * speed;
        
        // SimpleMove řeší gravitaci za tebe, takže postava nebude levitovat
        controller.SimpleMove(move);
    }
}