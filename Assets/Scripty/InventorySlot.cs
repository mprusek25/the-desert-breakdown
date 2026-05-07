using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image ikonaSlotu;
    private Predmet aktualniPredmet;

    public void NastavitSlot(Predmet novy) {
        aktualniPredmet = novy;
        ikonaSlotu.sprite = novy.ikona;
        ikonaSlotu.enabled = true;
    }

    public void VymazatSlot() {
        aktualniPredmet = null;
        ikonaSlotu.sprite = null;
        ikonaSlotu.enabled = false;
    }

    public void PouzitPredmet() {
        if (aktualniPredmet != null) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Inventar>().Pouzij(aktualniPredmet);
        }
    }
}