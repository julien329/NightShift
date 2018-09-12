using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class AmbulancePatientSpawner : BasePatientSpawner
    {
        public int LastMinFramesBeforeSpawn = 240;
        public int LastMaxFramesBeforeSpawn = 360;

        private int randomFramesBeforeSpawn;

        private int numberOfExplosionPatientsSpawned = 0;

        // Use this for initialization
        void Start()
        {
            randomFramesBeforeSpawn = (int)Random.Range(LastMinFramesBeforeSpawn, LastMaxFramesBeforeSpawn);
        }

        // Update is called once per frame
        void Update()
        {
            if (canSpawnPatient())
            {
                switch (GameFlowManager.GamePhase)
                {
                    case GameFlowState.PhaseTutorial:
                        break;
                    case GameFlowState.PhaseEasy:
                        break;
                    case GameFlowState.PhaseMedium:
                        break;
                    case GameFlowState.PhaseHard:
                        break;
                    case GameFlowState.LastPhase:
                        if(numberOfExplosionPatientsSpawned < 10)
                        {
                            createRandomPatient();
                            randomFramesBeforeSpawn = 30;
                            numberOfExplosionPatientsSpawned++;
                        }
                        else
                        {
                            createRandomPatient();
                        }
                        
                        break;
                    case GameFlowState.End:
                        break;
                }
            }
        }

        protected override bool canSpawnPatient()
        {
            if (randomFramesBeforeSpawn > 0)
            {
                randomFramesBeforeSpawn--;
                return false;
            }
            else
            {
                randomFramesBeforeSpawn = Mathf.Clamp((int)(Random.value * LastMaxFramesBeforeSpawn), LastMinFramesBeforeSpawn, LastMaxFramesBeforeSpawn);
                return true;
            }
        }

        private void createRandomPatient()
        {
            var rand = (int)(Random.Range(0, 10));
            Patient patient = null;
            switch (rand)
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
            if(patient != null && gameManager != null)
            {
                patient.gameManager = gameManager;
                //Add Patient to patient list
                GameManager.AddPatient(patient);
            }
        }
    }
}
