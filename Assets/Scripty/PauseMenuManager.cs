using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuManager : MonoBehaviour
{
    public static bool isPaused = false;

    [Header("Panely")]
    public GameObject pauseMenuUI;
    public GameObject autoSaveWarning;

    [Header("Ukládání")]
    public SaveSystem saveSystem; // TOTO PŘIDÁVÁME

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void SaveGame()
    {
        if (saveSystem != null)
        {
            saveSystem.SavePlayerData();
            Debug.Log("Hra manuálně uložena.");
        }
    }

    public void ExitToMenu()
    {
        if (saveSystem != null)
        {
            saveSystem.SavePlayerData(); // Auto-save před odchodem
        }
        Time.timeScale = 1f;
        StartCoroutine(ShowAutoSaveAndExit());
    }

    IEnumerator ShowAutoSaveAndExit()
    {
        pauseMenuUI.SetActive(false);
        autoSaveWarning.SetActive(true);
        yield return new WaitForSecondsRealtime(10f);
        SceneManager.LoadScene("MainMenu");
    }
}