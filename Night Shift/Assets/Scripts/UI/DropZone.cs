using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    static Patient currentPatient;
    static Color selected = new Color(0, 1f, 0.2f, 0.4f);
    static Color unselected = new Color(0, 0, 0, 0);


    void Start()
    {
        transform.parent.localPosition = new Vector3(0, 0, 1f);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Item d = eventData.pointerDrag.GetComponent<Item>();
        if (d != null)
        {
            Debug.Log("Worked!");
            //Get the patient gamescript to use the item
            Patient patient = gameObject.transform.parent.parent.gameObject.GetComponent<Patient>();
            if (patient == null)
                Debug.Log("Null px");
            //He may use the item if he is seated only
            if(patient.IsSeated || patient.State == PatientState.GettingTreated || patient.TransportedByPlayer)
            {
                Debug.Log("WasSeated!");
                d.UseItem(patient);
            }
                
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(currentPatient != null)
            PlayerFlags.IdOfLastClickedPatient = currentPatient.ID;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");
        Patient p = transform.parent.parent.gameObject.GetComponent<Patient>();
        if (p != null)
        {
            currentPatient = p;
            GetComponent<Image>().color = selected;
        }
             

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Exit");
        Patient p = transform.parent.parent.gameObject.GetComponent<Patient>();
        if(p != null)
        {
            //if (p == currentPatient)
            //{
                GetComponent<Image>().color = unselected;
                currentPatient = null;
            //}
                
        }
    }
}
