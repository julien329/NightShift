using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : GitCharacterController
{
    Patient currentPatient;

    //TreatmentZone highlights
    public SpriteRenderer[] treatmentZones;
    Color goTo = new Color(0.8f, 1f, 0f, 0.4f);


    // Use this for initialization
    void Start()
    {
        initialize();
        name = "Player";
        speed = 12.0f;
        currentPatient = null;
    }

    // Update is called once per frame
    void Update()
    {
        try {
            // Mouse button 0 is left click
            if (Input.GetMouseButton(0) && !PlayerFlags.disableLeftClick)
            {
                var newTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newTarget.z = transform.position.z;
                if (newTarget.x < 20.0f && newTarget.x > -20.0f && newTarget.y < 20.0f && newTarget.y > -20.0f)
                {
                    target = newTarget;
                }
            }

            /*
            if (PlayerFlags.isPlayerBeingFollowed)
            {
                speed = 6.0f;
            }
            else
            {
                speed = 12.0f;
            }
            */
            PlayerFlags.playerPosition = transform.position;

            hackMoveToPosition();
        }
        catch(Exception e)
        {
            //Because we hate exceptions
            print("Exception " + e.Message);
        }
    }

    public void StartCarrying(Patient patient)
    {
        speed = 8.0f;
        currentPatient = patient;

        switch (patient.Wound)
        {
            case (PatientWounds.Dead):
                treatmentZones[0].color = goTo;
                break;
            case (PatientWounds.Hemorhagie):
                treatmentZones[1].color = goTo;
                break;
            case (PatientWounds.Psychology):
                treatmentZones[2].color = goTo;
                break;
            case (PatientWounds.Vomitorium):
                treatmentZones[3].color = goTo;
                break;
            case (PatientWounds.Surgery):
                treatmentZones[4].color = goTo;
                break;
            case (PatientWounds.Exorcism):
                treatmentZones[5].color = goTo;
                break;
        }
    }

    public void StopCarrying(Patient patient)
    {
        speed = 12.0f;
        currentPatient = null;

        foreach(SpriteRenderer rend in treatmentZones)
        {
            rend.color = new Color(0, 0, 0, 0);
        }
    }

    private void hackMoveToPosition()
    {
        var tempPath = pathFinding.FindPath(transform.position, target);
        if (tempPath.Count != 0)
        {
            path = tempPath;
            nextPosition = 0;
        }

        // If a path is available to move
        if (path.Count != 0)
        {
            animationController.SetBool("Moving", true);
            // If we haven't reached our goal yet
            if (nextPosition < path.Count)
            {
                var nextPos = path[nextPosition].worldPosition_;
                nextPos.z = transform.position.z;
                // If we haven't reached the position of this node
                if (transform.position != nextPos)
                {
                    RotateToPoint(transform.position, nextPos);
                    var oldPos = transform.position;
                    transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
                    if (name == "Player")
                    {

                    }

                }
                else
                {
                    // Otherwise, we seek the next position
                    nextPosition++;
                    if (nextPosition < path.Count)
                    {
                        RotateToPoint(transform.position, nextPos);
                        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
                    }
                }
            }
            else
            {
                // We reached our goal, we can reset the path
                path = new List<Node>();
                nextPosition = 0;
                animationController.SetBool("Moving", false);
            }
        }
    }
}
