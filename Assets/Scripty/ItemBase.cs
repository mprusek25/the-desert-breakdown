using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public enum TypItemu { Jidlo, Voda, HP }
    public TypItemu coToJe;
    public float hodnotaPridani = 20f;

    public void Interact()
    {
        // Najdeme skript Staty na hráči
        Staty hracovyStaty = GameObject.FindGameObjectWithTag("Player").GetComponent<Staty>();

        if (hracovyStaty != null)
        {
            if (coToJe == TypItemu.Jidlo) hracovyStaty.jidlo += hodnotaPridani;
            if (coToJe == TypItemu.Voda) hracovyStaty.voda += hodnotaPridani;
            if (coToJe == TypItemu.HP) hracovyStaty.hp += hodnotaPridani;

            Debug.Log("Sebráno: " + coToJe + " +" + hodnotaPridani);
            Destroy(gameObject); // Předmět zmizí ze země
        }
        else
        {
            Debug.LogError("Chyba: Hráč nemá Tag 'Player' nebo mu chybí skript Staty!");
        }
    }
}