using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public Staty hracovyStaty;

    [Header("HP")]
    public Image hpBarVypln;
    public TextMeshProUGUI hpText;

    [Header("Jídlo")]
    public Image jidloBarVypln;
    public TextMeshProUGUI jidloText;

    [Header("Voda")]
    public Image vodaBarVypln;
    public TextMeshProUGUI vodaText;

    void Update()
    {
        if (hracovyStaty == null) return;

        // HP - Formát: 100/100
        if (hpBarVypln != null) hpBarVypln.fillAmount = hracovyStaty.hp / 100f;
        if (hpText != null) 
            hpText.text = Mathf.Ceil(hracovyStaty.hp).ToString() + "/100";

        // Jídlo - Formát: x%
        if (jidloBarVypln != null) jidloBarVypln.fillAmount = hracovyStaty.jidlo / 100f;
        if (jidloText != null) 
            jidloText.text = Mathf.Ceil(hracovyStaty.jidlo).ToString() + "%";

        // Voda - Formát: x%
        if (vodaBarVypln != null) vodaBarVypln.fillAmount = hracovyStaty.voda / 100f;
        if (vodaText != null) 
            vodaText.text = Mathf.Ceil(hracovyStaty.voda).ToString() + "%";
    }
}