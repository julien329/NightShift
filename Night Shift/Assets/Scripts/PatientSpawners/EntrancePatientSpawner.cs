using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class EntrancePatientSpawner : BasePatientSpawner
    {
        public int TutorialMinFramesBeforeSpawn = 240;
        public int TutorialMaxFramesBeforeSpawn = 540;

        public int EasyMinFramesBeforeSpawn = 240;
        public int EasyMaxFramesBeforeSpawn = 360;

        public int MediumMinFramesBeforeSpawn = 240;
        public int MediumMaxFramesBeforeSpawn = 300;

        public int HardMinFramesBeforeSpawn = 180;
        public int HardMaxFramesBeforeSpawn = 300;

        // Should not be different because of the Ambulance spawn
        public int LastMinFramesBeforeSpawn = 120;
        public int LastMaxFramesBeforeSpawn = 300;

        private int currentMinFramesBeforeSpawn;
        private int currentMaxFramesBeforeSpawn;

        private int randomFramesBeforeSpawn;

        // Use this for initialization
        void Start()
        {
            switch (GameFlowManager.GamePhase)
            {
                case GameFlowState.PhaseTutorial:
                    currentMinFramesBeforeSpawn = TutorialMinFramesBeforeSpawn;
                    currentMaxFramesBeforeSpawn = TutorialMaxFramesBeforeSpawn;
                    randomFramesBeforeSpawn = (int)Random.Range(currentMinFramesBeforeSpawn, currentMaxFramesBeforeSpawn);
                    break;
                case GameFlowState.PhaseEasy:
                    currentMinFramesBeforeSpawn = EasyMinFramesBeforeSpawn;
                    currentMaxFramesBeforeSpawn = EasyMaxFramesBeforeSpawn;
                    randomFramesBeforeSpawn = (int) Random.Range(currentMinFramesBeforeSpawn, currentMaxFramesBeforeSpawn);
                    break;
                case GameFlowState.PhaseMedium:
                    currentMinFramesBeforeSpawn = MediumMinFramesBeforeSpawn;
                    currentMaxFramesBeforeSpawn = MediumMaxFramesBeforeSpawn;
                    randomFramesBeforeSpawn = (int)Random.Range(currentMinFramesBeforeSpawn, currentMaxFramesBeforeSpawn);
                    break;
                case GameFlowState.PhaseHard:
                    currentMinFramesBeforeSpawn = HardMinFramesBeforeSpawn;
                    currentMaxFramesBeforeSpawn = HardMaxFramesBeforeSpawn;
                    randomFramesBeforeSpawn = (int)Random.Range(currentMinFramesBeforeSpawn, currentMaxFramesBeforeSpawn);
                    break;
                case GameFlowState.LastPhase:
                    currentMinFramesBeforeSpawn = LastMinFramesBeforeSpawn;
                    currentMaxFramesBeforeSpawn = LastMaxFramesBeforeSpawn;
                    randomFramesBeforeSpawn = (int)Random.Range(currentMinFramesBeforeSpawn, currentMaxFramesBeforeSpawn);
                    break;
                case GameFlowState.End:
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (canSpawnPatient())
            {
                Patient patient = null;
                switch (GameFlowManager.GamePhase)
                {
                    case GameFlowState.PhaseTutorial:
                        
                    case GameFlowState.PhaseEasy:
                        var randEasy = (int)(Random.Range(0, 4));
                        switch (randEasy)
                        {
                            case 0:
                                patient = (Instantiate(StevePatientCrazyPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 1:
                                patient = (Instantiate(StevePatientBloodPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 2:
                                patient = (Instantiate(AdamPatientCrazyPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 3:
                                patient = (Instantiate(AdamPatientBloodPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                        }
                        break;
                    case GameFlowState.PhaseMedium:
                        var randMedium = (int)(Random.Range(0, 8));
                        switch (randMedium)
                        {
                            case 0:
                                patient = (Instantiate(StevePatientKnifePrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 1:
                                patient = (Instantiate(StevePatientCrazyPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 2:
                                patient = (Instantiate(StevePatientBloodPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 3:
                                patient = (Instantiate(StevePatientVomitPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 4:
                                patient = (Instantiate(AdamPatientKnifePrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 5:
                                patient = (Instantiate(AdamPatientCrazyPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 6:
                                patient = (Instantiate(AdamPatientBloodPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 7:
                                patient = (Instantiate(AdamPatientVomitPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                        }
                        break;
                    case GameFlowState.PhaseHard:
                        var randHard = (int)(Random.Range(0,10));
                        switch (randHard)
                        {
                            case 0:
                                patient = (Instantiate(StevePatientCrazyPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 1:
                                patient = (Instantiate(StevePatientKnifePrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 2:
                                patient = (Instantiate(StevePatientBloodPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 3:
                                patient = (Instantiate(StevePatientVomitPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 4:
                                patient = (Instantiate(StevePatientPossessedPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 5:
                                patient = (Instantiate(AdamPatientKnifePrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 6:
                                patient = (Instantiate(AdamPatientCrazyPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 7:
                                patient = (Instantiate(AdamPatientBloodPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 8:
                                patient = (Instantiate(AdamPatientVomitPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 9:
                                patient = (Instantiate(AdamPatientPossessedPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                        }
                        break;
                    case GameFlowState.LastPhase:
                        var randLast = (int)(Random.Range(0, 10));
                        switch (randLast)
                        {
                            case 0:
                                patient = (Instantiate(StevePatientCrazyPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 1:
                                patient = (Instantiate(StevePatientKnifePrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 2:
                                patient = (Instantiate(StevePatientBloodPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 3:
                                patient = (Instantiate(StevePatientVomitPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 4:
                                patient = (Instantiate(StevePatientPossessedPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 5:
                                patient = (Instantiate(AdamPatientKnifePrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 6:
                                patient = (Instantiate(AdamPatientCrazyPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 7:
                                patient = (Instantiate(AdamPatientBloodPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 8:
                                patient = (Instantiate(AdamPatientVomitPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                            case 9:
                                patient = (Instantiate(AdamPatientPossessedPrefab, transform.position, Quaternion.identity) as GameObject).transform.GetChild(0).gameObject.GetComponent<Patient>();
                                break;
                        }
                        break;
                    case GameFlowState.End:
                        break;
                }
                if (patient != null && gameManager != null)
                {
                    patient.gameManager = gameManager;

                    //Add Patient to patient list
                    GameManager.AddPatient(patient);
                }
            }
        }

        protected override bool canSpawnPatient()
        {
            if(randomFramesBeforeSpawn > 0)
            {
                randomFramesBeforeSpawn--;
                return false;
            }
            else
            {
                randomFramesBeforeSpawn = Mathf.Clamp((int)(Random.value * currentMaxFramesBeforeSpawn), currentMinFramesBeforeSpawn, currentMaxFramesBeforeSpawn);
                return true;
            }
        }
    }
}
