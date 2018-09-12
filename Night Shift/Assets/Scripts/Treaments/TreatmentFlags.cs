using UnityEngine;
using System.Collections;

public static class TreatmentFlags
{
    public static bool isHemorhagieOccupied { get; set;}
    public static bool isPsychologyOccupied { get; set; }
    public static bool isVomitoriumOccupied { get; set; }
    public static bool isSurgeryOccupied { get; set; }
    public static bool isExorcismOccupied { get; set; }

    public static readonly Vector3 VomitoriumPosition = new Vector3(-12.0f, 9.4f, -1.0f);
    public static readonly Vector3 SurgeryPosition = new Vector3(3.750f, 12.15f, -1.0f);
    public static readonly Vector3 PsychologyPosition = new Vector3(18.75f, 2f, -1.0f);
    public static readonly Vector3 HemorhagiePosition = new Vector3(17.25f, -5.0f, -1.0f);
    public static readonly Vector3 ExorcismPosition = new Vector3(3.35f, -13.6f, -1.0f);
    public static readonly Vector3 EntrancePosition = new Vector3(-20.0f, 1.0f, -1.0f);
}
 