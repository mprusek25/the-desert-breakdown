using UnityEngine;

public class Staty : MonoBehaviour
{
    [Header("Aktuální hodnoty")]
    public float hp = 100f;
    public float jidlo = 100f;
    public float voda = 100f;

    [Header("Rychlost ubývání (jednotky za sekundu)")]
    public float rychlostHladu = 0.5f;
    public float rychlostZizne = 0.7f;

    void Update()
    {
        // Jídlo a voda klesají v čase
        if (jidlo > 0) jidlo -= rychlostHladu * Time.deltaTime;
        if (voda > 0) voda -= rychlostZizne * Time.deltaTime;

        // Pokud máš hlad nebo žízeň na nule, ubírá to HP
        if (jidlo <= 0 || voda <= 0)
        {
            hp -= 2f * Time.deltaTime; // Ubírá 2 HP za sekundu
        }

        // Zamezení záporným hodnotám
        hp = Mathf.Clamp(hp, 0, 100);
        jidlo = Mathf.Clamp(jidlo, 0, 100);
        voda = Mathf.Clamp(voda, 0, 100);
    }
}