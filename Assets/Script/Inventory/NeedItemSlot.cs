using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedItemSlot : MonoBehaviour
{
    [SerializeField] private string CodeItem_in_this_Slot;
    [SerializeField] private int Value_Need;
    [SerializeField] private int Value_Have;
    [SerializeField] Image pic_item;
    [SerializeField] Text value_text;
    Check_Item checkitem;

    public string SetCode
    {
        set { CodeItem_in_this_Slot = value; }
    }
    public int SetValueNeed
    {
        set { Value_Need = value; }
    }
    public int SetValueHave
    {
        set { Value_Have = value; }
    }
    void Start()
    {
        checkitem = (Check_Item)FindObjectOfType(typeof(Check_Item));
    }

    // Update is called once per frame
    void Update()
    {
        Chang_Pic();
    }

    void Chang_Pic()
    {
        value_text.text = Value_Have + " / " + Value_Need;
        for (int x = 0; x < checkitem.getitemcode_Length(); x++)
        {
            if (CodeItem_in_this_Slot == checkitem.getitemcode(x))
            {
                pic_item.sprite = checkitem.getpic(x);
            }
        }
    }
}
