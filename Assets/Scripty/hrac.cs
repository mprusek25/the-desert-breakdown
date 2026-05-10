using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Hrac : MonoBehaviour
{
    [Header("Nastavení pohybu")]
    public float rychlost = 6f;
    public float gravitace = -30f; // Silnější gravitace pro lepší pocit z dopadu
    public float vyskaSkoku = 1.5f;
    public float nasobicSprintu = 1.6f;

    [Header("Nastavení kamery")]
    public Camera mojeKamera;
    public float citlivostMysi = 2f;
    public float limitKamery = 85f;

    [Header("Model a Animace")]
    public Animator anim;

    private CharacterController controller;
    private Vector3 rychlostDopadu;
    private float rotaceX = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        // Uzamčení kurzoru
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (mojeKamera == null) mojeKamera = Camera.main;
    }

    void Update()
    {
        // Kontrola, aby se hrac nehybal, kdyz jsi v menu (pokud ho budes mit)
        if (Cursor.lockState != CursorLockMode.Locked) return;

        HandlePohled();
        HandlePohyb();
    }

    void HandlePohled()
    {
        float mysX = Input.GetAxis("Mouse X") * citlivostMysi;
        float mysY = Input.GetAxis("Mouse Y") * citlivostMysi;

        // Rotace tela do stran
        transform.Rotate(Vector3.up * mysX);

        // Rotace kamery nahoru a dolu
        rotaceX -= mysY;
        rotaceX = Mathf.Clamp(rotaceX, -limitKamery, limitKamery);
        mojeKamera.transform.localRotation = Quaternion.Euler(rotaceX, 0, 0);
    }

    void HandlePohyb()
    {
        // Kontrola jestli stojime na zemi
        bool jeZemi = controller.isGrounded;
        if (jeZemi && rychlostDopadu.y < 0)
        {
            rychlostDopadu.y = -2f;
        }

        // Vstupy z klavesnice
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // Výpočet směru
        Vector3 smer = (transform.right * x + transform.forward * z).normalized;

        // --- SPRINT LOGIKA ---
        float aktualniRychlost = rychlost;
        bool drziSprint = Input.GetKey(KeyCode.LeftShift);

        if (drziSprint && z > 0) // Sprintujeme jen kdyz jdeme dopredu
        {
            aktualniRychlost = rychlost * nasobicSprintu;
            if(anim != null) anim.speed = 1.5f; // Zrychli animaci chuze
        }
        else
        {
            if(anim != null) anim.speed = 1.0f; // Normalni rychlost animace
        }

        // Samotný pohyb
        controller.Move(smer * aktualniRychlost * Time.deltaTime);

        // --- ANIMACE ---
        if (anim != null)
        {
            bool hybeSe = smer.magnitude > 0.1f;
            // Nastavujeme isWalking podle toho, jestli se hybeme
            if (anim.GetBool("isWalking") != hybeSe)
            {
                anim.SetBool("isWalking", hybeSe);
            }
        }

        // --- SKOK ---
        if (Input.GetButtonDown("Jump") && jeZemi)
        {
            rychlostDopadu.y = Mathf.Sqrt(vyskaSkoku * -2f * gravitace);
            // Pokud mas v animatoru trigger pro skok, odkomentuj radek nize:
            // if(anim != null) anim.SetTrigger("doJump");
        }

        // Gravitace (pád)
        rychlostDopadu.y += gravitace * Time.deltaTime;
        controller.Move(rychlostDopadu * Time.deltaTime);
    }
}