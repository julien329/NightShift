using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public abstract class Item : Draggable, IPointerEnterHandler, IPointerExitHandler
{
    public string itemName;
    public string description;
    public int hpMod;
    public int calmMod;
    public int price;
    public GameManager gameManager;

    public override void OnBeginDrag(PointerEventData eventData)
    {

        base.OnBeginDrag(eventData);

    }

    public override void OnDrag(PointerEventData eventData)
    {

        base.OnDrag(eventData);

    }

    public override void OnEndDrag(PointerEventData eventData)
    {

        Debug.Log("EndDrag Items called");
        base.OnEndDrag(eventData);
        HideItemDesc();
    }

    //Pointer Enter and Exit function for items
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
            return;
        ShowItemDesc();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
            return;
        HideItemDesc();
    }

    void ShowItemDesc()
    {
        GameObject infoZone = GameObject.Find("InfoZone");
        //Set title
        infoZone.transform.GetChild(0).GetComponent<Text>().text = itemName + " - " + price + "$";

        //Set description
        infoZone.transform.GetChild(1).GetComponent<Text>().text = description;
    }

    void HideItemDesc()
    {
        GameObject infoZone = GameObject.Find("InfoZone");

        //Set title
        infoZone.transform.GetChild(0).GetComponent<Text>().text = "";

        //Set description
        infoZone.transform.GetChild(1).GetComponent<Text>().text = "";
    }

    public virtual void UseItem(Patient patient)
    {
        if(gameManager.Money < price)
        {
            Debug.Log("Broke");
            return;
        }

        //Charge the price
        gameManager.ChangeMoney(-price);

        //Hide the text
        HideItemDesc();

        //Affect the patient
        patient.HP += hpMod;
        patient.Calm += calmMod;
        patient.SpawnReward(-price, new Color(1, 0, 0, 0));
        

    }
}

