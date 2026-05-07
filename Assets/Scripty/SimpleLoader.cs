using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SmartLoader : MonoBehaviour
{
    [Header("Nastavení")]
    public float dobaLoadingu = 3f; // Jak dlouho uvidí nápis Načítání

    void Start()
    {
        StartCoroutine(ExecuteLoading());
    }

    IEnumerator ExecuteLoading()
    {
        // Počkáme nastavený čas
        yield return new WaitForSeconds(dobaLoadingu);

        // Zjistíme z paměti, kam nás SaveSlotManager poslal
        // Pokud tam nic není (chyba), pošle nás to do SampleScene
        string nextScene = PlayerPrefs.GetString("AfterLoadingGoTo", "SampleScene");

        Debug.Log("Loading dokončen, směr: " + nextScene);
        SceneManager.LoadScene(nextScene);
    }
}