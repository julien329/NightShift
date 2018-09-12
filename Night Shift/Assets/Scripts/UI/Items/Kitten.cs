using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Kitten : Item
{
    void Start()
    {
        itemName = "Kitten";
        description = "You know it has reached its petting capacity when it starts clawing back. \n --- \n +50 Calm \n -10 HP";
    }
}
