using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Tranquilizer : Item
{
    void Start()
    {
        itemName = "Horse Sedative";
        description = "When is the last time we even treated a horse? \n --- \n +100 Calm \n -25 HP";
    }
}
