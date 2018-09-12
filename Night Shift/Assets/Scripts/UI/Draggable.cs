using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public abstract class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

    public Transform parentToReturnTo = null;
    public Transform placeholderParent = null;

    public GameObject placeholder = null;

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Base");
        //Remember this will return to position as basic value
        parentToReturnTo = this.transform.parent;
        //Set the item free
        PlayerFlags.disableLeftClick = true;
        GameManager.enableCanvasForPatients();

        this.transform.SetParent(this.transform.parent.parent.parent);
        this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        this.transform.position = eventData.position;

        //Set placeholder for return/Swap
        placeholder = new GameObject();
        placeholder.name = "placeholder";
        placeholder.transform.SetParent(parentToReturnTo);
        placeholderParent = parentToReturnTo;

    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;

    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {        
        this.transform.SetParent(parentToReturnTo);
        this.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        PlayerFlags.disableLeftClick = false;
        GameManager.disableCanvasForPatients();

        Destroy(placeholder);

    }

    public void returnToPosition()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy(placeholder);
    }

}
