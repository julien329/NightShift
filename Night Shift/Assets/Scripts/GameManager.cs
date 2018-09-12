using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //Patients list
    static List<Patient> patients;

    //Game Metrics
    int money;
    public int Money { get { return money; } }

    int reputation;
    int deathCount;
    AudioSource audioSource;
    public List<AudioClip> audioclips;
    public AudioClip gong;

    public int curedMoneyReward;
    public int curedRepReward;
    public int deathMoneyPenalty;

    public int hemorhagieCalmLost = -3;
    public int psychologyCalmLost = -5;
    public int surgeryCalmLost = -5;
    public int vomitoriumCalmLost = -2;
    public int exorcismCalmLost = -7;

    public Text moneyText;
    public Text deathText;
    public GameObject repBar;

    // Black and yellow doors
    public GameObject doorMed1;
    public GameObject doorMed2;
    public GameObject doorHard;

    //IconLinks
    public GameObject[] items;

    //Time management - 60seconds = 1 round
    int playTime;
    public int secondsForUpdates;
    public GameObject minTic;
    public GameObject hourTic;

    // Use this for initialization
    void Start() {

        audioSource = GetComponent<AudioSource>();
        //DEBUG***********************************/
        money = 20;
        reputation = 70;
        deathCount = 0;
        playTime = 0;
        UpdateMoneyDisplay();
        UpdateDeathDisplay();
        UpdateRepDisplay();
        UpdateItemDisplay();
        //****************************************/
        patients = new List<Patient>();
        InvokeRepeating("SecondPassed", 1f, 1f);
        StartCoroutine(PlayRandomSounds());
    }

    IEnumerator PlayRandomSounds() {
        while(true) {
            yield return new WaitForSeconds(Random.Range(15, 30));
            audioSource.clip = audioclips[Random.Range(0, audioclips.Count - 1)];
            audioSource.Play();
        }
    }

    //Generates reputation bar color
    Color GetColor()
    {
        Color barColor;

        if (reputation > 50)
        {
            barColor = new Color((100 - reputation) / 50f, 1f, 0f);
        }
        else
            barColor = new Color(1f, reputation / 50f, 0f);

        return barColor;
        
    }

    //Changes Money and updates the display
	public void ChangeMoney(int amount)
    {
        money += amount;
        UpdateMoneyDisplay();
        UpdateItemDisplay();

        if(money < -100)
            LoseGame();
    }

    //Changes reputation and updates the display
    public void ChangeRep(int amount)
    {
        reputation += amount;

        if (reputation > 100)
            reputation = 100;

        //Lose condition
        else if (reputation < 0)
        {
            reputation = 0;
            UpdateRepDisplay();
            LoseGame();
        }

        UpdateRepDisplay();
    }

    //Called when a patient has healed. Increases money and reputation
    public void PatientCured()
    {
        ChangeMoney(curedMoneyReward);
        ChangeRep(curedRepReward);
    }

    //Changes death count and updates the display
    public void PatientDied()
    {
        deathCount++;
        UpdateDeathDisplay();
        ChangeMoney(-deathMoneyPenalty);
        ChangeRep(-deathCount);
    }
    
    //Display updates
    void UpdateMoneyDisplay()
    {
        //Update money display
        moneyText.text = money.ToString() + "$";
        if (money < 0)
            moneyText.color = Color.red;
        else
            moneyText.color = Color.white;

    }

    void UpdateRepDisplay()
    {
        //Update reputation
        float size = reputation / 100f;
        repBar.transform.localScale = new Vector3(size, 0.25f);
        repBar.GetComponent<Image>().color = GetColor();
    }

    void UpdateDeathDisplay()
    {
        //Update death text
        deathText.text = deathCount.ToString();
        int fontSize = deathCount * 2 + 12;
        if (fontSize > 36)
            fontSize = 36;
        deathText.fontSize = fontSize;
    }

    void UpdateItemDisplay()
    {
        for(int i = 0; i < items.Length; i++)
        { 
            if (items[i].GetComponent<Item>().price > money)
                items[i].GetComponent<Image>().color = Color.black;
            else
                items[i].GetComponent<Image>().color = Color.white;
        }
    }

    //Time management
    void SecondPassed()
    {
        playTime++;
        minTic.transform.Rotate(Vector3.back, 6f);
        hourTic.transform.Rotate(Vector3.back, 0.5f);

        int amountOfCalmLost = 0;
        int amountOfCorpses = 0;

        foreach (Patient patient in patients)
        {
            if(patient.Wound == PatientWounds.Dead)
            {
                amountOfCorpses++;
            }
        }

        
        foreach (Patient patient in patients)
        {
            amountOfCalmLost += GameManager.StatLost(reputation);
            amountOfCalmLost -= amountOfCorpses;
            patient.Calm = Mathf.Clamp(patient.Calm, 0, 100);

            switch (patient.Wound)
            {
                case PatientWounds.Healthy:
                    continue;
                case PatientWounds.Dead:
                    continue;
                case PatientWounds.Hemorhagie:
                    amountOfCalmLost += hemorhagieCalmLost;
                    break;
                case PatientWounds.Psychology:
                    amountOfCalmLost += psychologyCalmLost;
                    break;
                case PatientWounds.Vomitorium:
                    amountOfCalmLost += vomitoriumCalmLost;
                    break;
                case PatientWounds.Surgery:
                    amountOfCalmLost += surgeryCalmLost;
                    break;
                case PatientWounds.Exorcism:
                    amountOfCalmLost += exorcismCalmLost;
                    break;
            }

            patient.UpdateStatsPatient(amountOfCalmLost);
        }
        

        //StateSwitch
        if(playTime%60 == 0)
        {
            if (GameFlowManager.GamePhase == GameFlowState.LastPhase)
                WinGame();
            GameFlowManager.GamePhase++;
            audioSource.clip = gong;
            audioSource.Play();

            if(GameFlowManager.GamePhase == GameFlowState.PhaseMedium) {
                Destroy(doorMed1);
                Destroy(doorMed2);
            }
            if (GameFlowManager.GamePhase == GameFlowState.PhaseHard) {
                Destroy(doorHard);
            }
            


        }
    }

    //Manage the static list of patients
    static public void AddPatient(Patient patient)
    {
        patients.Add(patient);
    }

    static public void RemovePatient(Patient patient)
    {
        patients.Remove(patient);
    }

    //Takes independant value and passes it in StatLost function
    static public int StatLost(int value)
    {
        if (value <= 5)
            return -60;
        else if (value <= 20)
            return -30;
        else if (value <= 40)
            return -15;
        else if (value <= 60)
            return -10;
        else if (value <= 80)
            return -5;
        else
            return -2;
    }

    //Is called when a lose condition is met (reputation of 0, too much debt)
    void LoseGame()
    {
        SceneManager.LoadScene("Defeat");
    }

    //Is called when the time is over
    static public void WinGame()
    {
        SceneManager.LoadScene("Victory");
    }

    public static void disableCanvasForPatients()
    {
        /*
        foreach(Patient patient in patients)
        {
            (patient.transform.GetChild(0)).position = new Vector3(-1000, -1000, -1000);
        }
        */
    }

    public static void enableCanvasForPatients()
    {
        /*
        foreach (Patient patient in patients)
        {
            (patient.transform.GetChild(0)).position = patient.transform.position;
        }
        */
    }

   
}
