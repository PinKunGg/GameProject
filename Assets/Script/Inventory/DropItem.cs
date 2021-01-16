using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItem : MonoBehaviour, IDropHandler
{
    [SerializeField] CreateInventory inven;
    [SerializeField] InvenSlot invenSlot;
    [SerializeField] GameObject this_slot;

    void Start() 
    {
        inven = (CreateInventory)FindObjectOfType(typeof(CreateInventory));
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (inven.GetComponent<CreateInventory>().select_slot != null) 
        {
            Debug.Log("Drop + " + invenSlot.GetComponent<InvenSlot>().Set_SlotNumber);
            inven.Check_tpye_for_swap(this_slot);
            //inven.Swap_Slot(this_slot);
        }
    }
}
