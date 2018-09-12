using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;
    private float timeScaleRef;
    private float volumeRef;
    private bool paused;

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
            MenuStatusChange();
    }


    private void MenuOn() {
        timeScaleRef = Time.timeScale;
        Time.timeScale = 0f;

        volumeRef = AudioListener.volume;
        AudioListener.volume = 0f;
        AudioListener.pause = true;

        pauseMenu.SetActive(true);
        paused = true;
    }


    public void MenuOff() {
        Time.timeScale = timeScaleRef;
        AudioListener.volume = volumeRef;
        AudioListener.pause = false;

        pauseMenu.SetActive(false);
        paused = false;
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void MenuStatusChange() {
        if (paused)
            MenuOff();
        else
            MenuOn();
    }

    public void LoadScene(string level) {
        SceneManager.LoadScene(level);
    }
}
