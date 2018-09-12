using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class PlayerFlags : MonoBehaviour
{
    public static bool isPlayerBeingFollowed { get; set; }

    public static Vector3 playerPosition { get; set; }

    public static int IdOfLastClickedPatient = -1;

    public static bool disableLeftClick = false;
}
