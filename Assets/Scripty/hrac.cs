using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Hrac : MonoBehaviour
{
    [Header("Pohybové statistiky")]
    public float rychlost = 5f;
    public float gravitace = -9.81f;
    public float vyskaSkoku = 1.5f;

    [Header("Nastavení kamery")]
    public Camera mojeKamera;
    public float citlivostMysi = 2f;
    
    [Header("Sběr předmětů")]
    public float dosahSbirani = 3f;
    public LayerMask vrstvaProSbirani; // Volitelné: nastav na 'Everything' v Inspectoru

    private CharacterController controller;
    private Vector3 rychlostDopadu; // Pro gravitaci
    private float rotaceX = 0f;
    private bool jeZemi;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Skryjeme a zamkneme kurzor pro FPS ovládání
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (mojeKamera == null)
        {
            mojeKamera = Camera.main;
        }
    }

    void Update()
    {
        // 1. KONTROLA KURZORU
        // Pokud je inventář otevřený (kurzor je vidět), vypneme ovládání postavy
        if (Cursor.lockState == CursorLockMode.None) return;

        HandlePohyb();
        HandleRotace();
        HandleSbirani();
    }

    void HandlePohyb()
    {
        jeZemi = controller.isGrounded;
        if (jeZemi && rychlostDopadu.y < 0)
        {
            rychlostDopadu.y = -2f; // Drží hráče přilepeného k zemi
        }

        // Vstupy z klávesnice (WASD)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // POHYB SMĚREM, KAM SE DÍVÁM (transform.forward a right)
        Vector3 smerPohybu = transform.right * x + transform.forward * z;
        controller.Move(smerPohybu * rychlost * Time.deltaTime);

        // SKOK
        if (Input.GetButtonDown("Jump") && jeZemi)
        {
            rychlostDopadu.y = Mathf.Sqrt(vyskaSkoku * -2f * gravitace);
        }

        // Aplikace gravitace
        rychlostDopadu.y += gravitace * Time.deltaTime;
        controller.Move(rychlostDopadu * Time.deltaTime);
    }

    void HandleRotace()
    {
        // Vstupy z myši
        float mysX = Input.GetAxis("Mouse X") * citlivostMysi;
        float mysY = Input.GetAxis("Mouse Y") * citlivostMysi;

        // Vertikální rotace (Kamera se dívá nahoru/dolů)
        rotaceX -= mysY;
        rotaceX = Mathf.Clamp(rotaceX, -85f, 85f); // Zamezí protočení hlavy
        mojeKamera.transform.localRotation = Quaternion.Euler(rotaceX, 0f, 0f);

        // Horizontální rotace (Celé TĚLO hráče se otáčí doleva/doprava)
        transform.Rotate(Vector3.up * mysX);
    }

    void HandleSbirani()
    {
        // Pokud stisknu E
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Vystřelíme paprsek ze středu kamery
            Ray ray = mojeKamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, dosahSbirani))
            {
                // Pokud trefíme něco se skriptem Pickup
                Pickup p = hit.transform.GetComponent<Pickup>();
                if (p != null)
                {
                    p.Sebrat();
                    Debug.Log("Sebráno: " + hit.transform.name);
                }
            }
        }
    }
}