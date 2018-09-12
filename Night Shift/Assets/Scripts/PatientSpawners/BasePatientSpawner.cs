using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class BasePatientSpawner : MonoBehaviour {
        
        public GameObject StevePatientKnifePrefab;
        public GameObject StevePatientCrazyPrefab;
        public GameObject StevePatientBloodPrefab;
        public GameObject StevePatientVomitPrefab;
        public GameObject StevePatientPossessedPrefab;

        public GameObject AdamPatientKnifePrefab;
        public GameObject AdamPatientCrazyPrefab;
        public GameObject AdamPatientBloodPrefab;
        public GameObject AdamPatientVomitPrefab;
        public GameObject AdamPatientPossessedPrefab;

        public GameManager gameManager;

        // Use this for initialization
        void Start() {
        }

        // Update is called once per frame
        void Update() {
        }

        protected virtual bool canSpawnPatient()
        {
            return false;
        }
    }
}
