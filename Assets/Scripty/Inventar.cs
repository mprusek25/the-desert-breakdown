using System.Collections.Generic;
using UnityEngine;

public class Inventar : MonoBehaviour
{
    public List<Predmet> polozky = new List<Predmet>();
    public InventorySlot[] uiSloty; 
    public Staty hracovyStaty;

    // --- NOVÉ PROMĚNNÉ PRO OTEVÍRÁNÍ ---
    public GameObject oknoInventare; // Sem přetáhneš "InventarOkno"
    private bool jeOtevreno = false;

    void Start() {
        // Na začátku hry inventář schováme
        if(oknoInventare != null) oknoInventare.SetActive(false);
    }

    void Update() {
        // Přepínání Tabem
        if (Input.GetKeyDown(KeyCode.Tab)) {
            jeOtevreno = !jeOtevreno;
            oknoInventare.SetActive(jeOtevreno);

            // Odemčení/Zamčení myši
            if (jeOtevreno) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
    // --- KONEC NOVÉ ČÁSTI ---

    public void Pridej(Predmet p) {
        if (polozky.Count < uiSloty.Length) {
            polozky.Add(p);
            AktualizujUI();
        }
    }

    public void Pouzij(Predmet p) {
        hracovyStaty.hp += p.obnovaHP;
        hracovyStaty.jidlo += p.obnovaJidla;
        hracovyStaty.voda += p.obnovaVody;
        if (p.jeSpotrebitelny) polozky.Remove(p);
        AktualizujUI();
    }

    void AktualizujUI() {
        for (int i = 0; i < uiSloty.Length; i++) {
            if (i < polozky.Count) uiSloty[i].NastavitSlot(polozky[i]);
            else uiSloty[i].VymazatSlot();
        }
    }
}