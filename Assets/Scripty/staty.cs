using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Staty : MonoBehaviour
{
    [Header("Aktuální hodnoty")]
    public float hp = 100f;
    public float jidlo = 100f;
    public float voda = 100f;

    [Header("Rychlost ubývání")]
    public float rychlostHladu = 0.5f;
    public float rychlostZizne = 0.7f;

    [Header("UI Elementy")]
    public Image hpBar; public Image jidloBar; public Image vodaBar;
    public TextMeshProUGUI hpText; public TextMeshProUGUI jidloText; public TextMeshProUGUI vodaText;

    void Update()
    {
        // Ubývání
        if (jidlo > 0) jidlo -= rychlostHladu * Time.deltaTime;
        if (voda > 0) voda -= rychlostZizne * Time.deltaTime;

        if (jidlo <= 0 || voda <= 0) hp -= 2f * Time.deltaTime;

        // Omezení (0 - 100)
        hp = Mathf.Clamp(hp, 0, 100);
        jidlo = Mathf.Clamp(jidlo, 0, 100);
        voda = Mathf.Clamp(voda, 0, 100);

        AktualizujUI();
    }

    void AktualizujUI()
    {
        if (hpBar != null) hpBar.fillAmount = hp / 100f;
        if (jidloBar != null) jidloBar.fillAmount = jidlo / 100f;
        if (vodaBar != null) vodaBar.fillAmount = voda / 100f;

        if (hpText != null) hpText.text = hp.ToString("F0") + "%";
        if (jidloText != null) jidloText.text = jidlo.ToString("F0") + "%";
        if (vodaText != null) vodaText.text = voda.ToString("F0") + "%";
    }
}