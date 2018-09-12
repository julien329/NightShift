using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Defibrillator : Item
{
    void Start()
    {
        itemName = "Defibrillator";
        description = "Total eclipse of the heart. \n --- \n +100 HP \n -25 Calm";
    }
}
