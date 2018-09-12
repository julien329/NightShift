using UnityEngine;
using System.Collections;

public class Treatment : MonoBehaviour
{
    public TreatmentType treatmentType;

    void Start()
    {
        switch (treatmentType)
        {
            case TreatmentType.Hemorhagie:
                name = "Hemorhagie";
                break;
            case TreatmentType.Psychology:
                name = "Psychology";
                break;
            case TreatmentType.Vomitorium:
                name = "Vomitorium";
                break;
            case TreatmentType.Surgery:
                name = "Surgery";
                break;
            case TreatmentType.Exorcism:
                name = "Exorcism";
                break;
            case TreatmentType.Morgue:
                name = "Morgue";
                break;
            case TreatmentType.Entrance:
                name = "Entrance";
                break;
        }
    }
}

