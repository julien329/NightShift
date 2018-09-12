using UnityEngine;
using System.Collections;

public class StatBar : MonoBehaviour {

    public float radius;
    Patient patient;

    void Start()
    {
        patient = transform.parent.GetChild(0).gameObject.GetComponent<Patient>();
    }
	
	// Update is called once per frame
	void Update () {
        //Move bars
        gameObject.transform.position = gameObject.transform.parent.GetChild(0).position + new Vector3(0, radius);

        //Scale bar - radius 0.8 - hp, else its a calm bar -- Bad programming
        if (radius == 0.85f)
            gameObject.transform.localScale = new Vector3(patient.HP / 100f, 1);
        else
            gameObject.transform.localScale = new Vector3(patient.Calm / 100f, 1);


    }
}
