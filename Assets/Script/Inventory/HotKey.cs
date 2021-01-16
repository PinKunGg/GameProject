using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotKey : MonoBehaviour
{
    [SerializeField] string WhatItemCode;
    [SerializeField] int valueInSlot;
    [SerializeField] GameObject Whatslot;
    [SerializeField] Check_Item check_Item;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (valueInSlot == 0) 
        {
            WhatItemCode = check_Item.getitemcode(0);
        }
    }

    public void use_item() 
    {
        Debug.Log("Item : " + WhatItemCode);
        Debug.Log("Value : " + valueInSlot);
    }

    public void SelectHotKey(string code,int value,GameObject slot) 
    {
        WhatItemCode = code;
        Whatslot = slot;
        valueInSlot = value;
    }
    public void UpdateWhenSwitchHotKey() 
    {
        WhatItemCode = Whatslot.GetComponent<InvenSlot>().Set_CodeItem;
        valueInSlot = Whatslot.GetComponent<InvenSlot>().Set_Value;
    }
}
