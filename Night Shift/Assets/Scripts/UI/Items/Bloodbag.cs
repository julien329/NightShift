using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Bloodbag : Item
{
    void Start()
    {
        itemName = "Blood Bag";
        description = "It comes with a fuzzy straw! \n --- \n +50 HP \n -10 Calm";
    }
}
