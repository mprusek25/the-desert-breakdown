using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    public TextMeshProUGUI instrukceText; // Ten text "Stiskni cokoliv..."
    public string herniScena = "SampleScene";
    private bool muzeStartovat = false;

    void Start()
    {
        instrukceText.gameObject.SetActive(false);
        StartCoroutine(PockejNaStart());
    }

    IEnumerator PockejNaStart()
    {
        // Tady můžeš postupně měnit text v bublinách, jestli chceš
        yield return new WaitForSeconds(10f); // Těch tvých 10 sekund
        
        instrukceText.gameObject.SetActive(true);
        muzeStartovat = true;
    }

    void Update()
    {
        if (muzeStartovat && Input.anyKeyDown)
        {
            SceneManager.LoadScene(herniScena);
        }
    }
}