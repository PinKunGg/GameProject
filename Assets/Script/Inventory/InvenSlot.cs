using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler/*, IPointerEnterHandler*//*, IDropHandler*/
{
    [SerializeField] private int My_Slot_Number;
    [SerializeField] private string CodeItem_in_this_Slot = "#000-00";
    [SerializeField] private int Value_in_Slot;
    [SerializeField] private bool This_is_hotkey = false;
    //[SerializeField] private bool[] This_is_eiquiment_or_wepond = new bool[2];
    [SerializeField] private string what_code_item_can_get = "#00x-xx";
    private bool can_drag = true;

    [SerializeField] HotKey hotKey_script;
    [SerializeField] GameObject image;
    [SerializeField] Canvas canvas;
    [SerializeField] CreateInventory inven;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Text value_text;
    [SerializeField] Image pic_item;
    [SerializeField] Check_Item checkitem;

    #region SetGetOnly
    public int Set_SlotNumber 
    { 
        set { My_Slot_Number = value; }
        get { return My_Slot_Number; }
    }
    public string Set_CodeItem 
    { 
        set { CodeItem_in_this_Slot = value; }
        get { return CodeItem_in_this_Slot; }
    }
    public bool Set_hotkey
    {
        set { This_is_hotkey = value; }
        get { return This_is_hotkey; }
    }

    public int Set_Value
    {
        set { Value_in_Slot = value; }
        get { return Value_in_Slot; }
    }
    public bool Set_can_drag
    {
        set { can_drag = value; }
        get { return can_drag; }
    }

    public string Check_Equipment()
    {
        //for (int x = 0; x < This_is_eiquiment_or_wepond.Length; x++)
        //{
        //    if (This_is_eiquiment_or_wepond[x] == true)
        //    {
        //        return x + 4;
        //    }
        //}
        //return 1;
        return what_code_item_can_get;
    }

    public string get_pos_Equipment(int pos) 
    {
        char[] x = what_code_item_can_get.ToCharArray();
        return x[pos].ToString();
    }
    #endregion
    void Start()
    {
        inven = (CreateInventory)FindObjectOfType(typeof(CreateInventory));
        checkitem = (Check_Item)FindObjectOfType(typeof(Check_Item));
        if (what_code_item_can_get == "#00x-xx") 
        {
            this.gameObject.name = My_Slot_Number.ToString();
        }
    }

    void Update()
    {
        Check_Full();       
    }

    void Check_Full() //เช็คว่าช่องเต็มแล้วหรือยัง
    {
        if (Value_in_Slot > 0)
        {
            value_text.text = Value_in_Slot.ToString();
            if (This_is_hotkey == false) 
            {
                can_drag = true;
            }                     
        }
        else
        {
            CodeItem_in_this_Slot = checkitem.getitemcode(0);
            value_text.text = "";
            pic_item.sprite = checkitem.getpic(0);
            can_drag = false;
        }
        Chang_Pic();
    }

    void Chang_Pic() 
    {
        for (int x = 0; x < checkitem.getitemcode_Length(); x++)
        {           
            if (CodeItem_in_this_Slot == checkitem.getitemcode(x))
            {
                pic_item.sprite = checkitem.getpic(x);
                break;
            }
            else 
            {
                pic_item.sprite = checkitem.getpic(0);
            }
        }
    }

    public void LoadItem() 
    {
        CodeItem_in_this_Slot = inven.GetCode(My_Slot_Number - 1);
        Value_in_Slot = inven.GetValue(My_Slot_Number - 1);
    }

    public void AddItem(string CodeItem) //เพิ่มไอเท็มเข้าช่องนี้
    {      
        CodeItem_in_this_Slot = CodeItem;
        Value_in_Slot++;
        Debug.Log(My_Slot_Number);
    }

    public void OnBeginDrag(PointerEventData eventData) 
    {
        if (can_drag == true) 
        {
            Debug.Log("Enter + " + My_Slot_Number);
            inven.Select_Slot(this.gameObject);
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (can_drag == true) 
        {
            image.transform.position = Input.mousePosition;
            canvas.sortingOrder = 2;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.transform.localPosition = Vector3.zero;
        canvas.sortingOrder = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void Click() 
    {
        hotKey_script.SelectHotKey(CodeItem_in_this_Slot,Value_in_Slot,this.gameObject);
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    Debug.Log("Enter + " + My_Slot_Number);
    //}

    //public void OnDrop(PointerEventData eventData) 
    //{
    //    Debug.Log("Drop + " + My_Slot_Number);
    //}
}
