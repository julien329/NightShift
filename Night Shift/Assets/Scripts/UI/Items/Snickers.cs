using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Snickers : Item
{
    void Start()
    {
        itemName = "Snickers";
        description = "You look angry... Have a Snickers! \n --- \n +25 Calm";
    }
}
