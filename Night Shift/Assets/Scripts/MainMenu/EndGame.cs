using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    public string mainMenuScene = "MainMenu";
    public int targetTime = 5;
    public GameObject overlay;
    Animator animator;
    int time = 0;
        

	void Start () {
        animator = overlay.GetComponent<Animator>();
        InvokeRepeating("TimerBeforeMenu", 1, 1);
    }
	
	void TimerBeforeMenu() {
        time++;
        if (time == targetTime)
            StartCoroutine(FadeScreen());
    }

    IEnumerator FadeScreen() {
        animator.SetTrigger("FadeScreen");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(mainMenuScene);
    }
}
