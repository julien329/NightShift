using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject storyMenu;

    float timeSpent = 0f;
    Text brief;
    GameObject buttons;

    void Start()
    {
        brief = GameObject.Find("Briefing").GetComponent<Text>();
        buttons = GameObject.Find("Buttons");
    }


    public void SelectMainMenu() {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
        storyMenu.SetActive(false);
    }

    public void SelectCreditsMenu() {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void SelectStoryMenu() {
        mainMenu.SetActive(false);
        storyMenu.SetActive(true);
    }

    public void LoadScene(string level) {
        
        StartCoroutine("LaunchGame");
        
        
    }

    IEnumerator LaunchGame()
    {
        Image[] butts = buttons.GetComponentsInChildren<Image>();
        while (timeSpent < 3f)
        {
            timeSpent += 0.3f;
            brief.color = new Color(1, 1, 1, timeSpent / 3f);

            for(int i = 0; i < butts.Length; i++)
            {
                butts[i].color = new Color(1, 1, 1, 1 - (timeSpent / 3f));
            }

            yield return new WaitForSeconds(0.1f);

        }

        yield return new WaitForSeconds(14f);

        SceneManager.LoadScene("Game");
    }

    public void ApplicationQuit() {
        Application.Quit();
    }
}
