using UnityEngine;

public class Hrac : MonoBehaviour
{
    public float rychlost = 10f;
    public Transform teloModelu; // Tady přetáhneš ten body-mesh
    
    private CharacterController controller;
    private float timer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 pohyb = transform.right * x + transform.forward * z;
        pohyb.y = -9.81f; 

        controller.Move(pohyb * rychlost * Time.deltaTime);

        // --- HOPSÁNÍ PRO TYTO KRABICE ---
        if (teloModelu != null)
        {
            // Pokud se hýbeš (WASD)
            if (new Vector2(x, z).magnitude > 0.1f)
            {
                timer += Time.deltaTime * 10f; // Rychlost hopsání
                float yPos = Mathf.Abs(Mathf.Sin(timer)) * 0.2f; // Výška skoku
                teloModelu.localPosition = new Vector3(0, yPos, 0);
                
                // Bonus: trošku ho nakloníme při chůzi, ať je to víc "popiči"
                teloModelu.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(timer) * 5f);
            }
            else
            {
                // Když stojí, vrátí se na nulu
                timer = 0;
                teloModelu.localPosition = Vector3.zero;
                teloModelu.localRotation = Quaternion.identity;
            }
        }
    }
}