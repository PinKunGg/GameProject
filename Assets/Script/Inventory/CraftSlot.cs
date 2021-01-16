using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour
{
    public Button This_Button;
    Check_Item check_Item;
    CreateInventory createinven;
    [SerializeField] private string CodeItem;
    [SerializeField] List<string> itemForCraft_code = new List<string>();
    [SerializeField] List<int> itemForCraft_Value = new List<int>();
    [SerializeField] Image pic_item;
    [SerializeField] Transform Prefab_Inven_slot;
    GameObject Inven_slot_obj;
    GameObject[] NeedItem;
    bool first = true;



    public string SetCodeItem
    {
        set { CodeItem = value; }
        get { return CodeItem; }
    }
    void Start()
    {
        createinven = (CreateInventory)FindObjectOfType(typeof(CreateInventory));
        check_Item = (Check_Item)FindObjectOfType(typeof(Check_Item));
        This_Button = this.gameObject.GetComponent<Button>();
        This_Button.interactable = false;
        Check_Item_For_Craft();
        Chang_Pic();
    }

    void Chang_Pic()
    {
        for (int x = 0; x < check_Item.getitemcode_Length(); x++)
        {
            if (CodeItem == check_Item.getitemcode(x))
            {
                pic_item.sprite = check_Item.getpic(x);
            }
        }
    }

    private void Update()
    {
        Check_Can_Craft();
    }
    void Check_Item_For_Craft() 
    {
        int count = check_Item.getitemcraft_Lenght(CodeItem);
        for (int x = 0; x < count; x++) 
        {
            if (x < count / 2)
            {
                Debug.Log("Code : "+check_Item.CheckCraft(CodeItem, x));
                itemForCraft_code.Add(check_Item.CheckCraft(CodeItem, x));
                //pic_item.sprite = check_Item.getpic(x);
            }
            else
            {
                Debug.Log("Value : " + int.Parse(check_Item.CheckCraft(CodeItem, x)));
                itemForCraft_Value.Add(int.Parse(check_Item.CheckCraft(CodeItem, x)));
            }
        }
        NeedItem = new GameObject[itemForCraft_code.Count];
    }

    void Check_Can_Craft() 
    {
        if (first == true)
        {
            Create_Need_Item();
            first = false;
        }
        int check = 0;
        for (int x = 0; x < itemForCraft_code.Count; x++) 
        {
            for (int i = 0; i < check_Item.getitemcode_Length() ; i++) 
            {
                if (itemForCraft_code[x] == check_Item.getitemcode(i))
                {
                    NeedItem[x].GetComponent<NeedItemSlot>().SetValueHave = check_Item.getitemvalue(i);
                    if (itemForCraft_Value[x] <= check_Item.getitemvalue(i)) 
                    {                    
                        check++;
                        break;
                    }
                }
            }
           This_Button.interactable = (check == itemForCraft_code.Count);
        }       
    }

    private void Create_Need_Item()  //สร้างช่องเก็บของ
    {
        Inven_slot_obj = Prefab_Inven_slot.GetChild(0).gameObject;       
        for (int i = 0; i < itemForCraft_code.Count; i++) //สร้างช่องทั่วไป
        {
            NeedItem[i] = Instantiate(Inven_slot_obj, Prefab_Inven_slot);
            NeedItem[i].GetComponent<NeedItemSlot>().SetCode = itemForCraft_code[i];
            NeedItem[i].GetComponent<NeedItemSlot>().SetValueNeed = itemForCraft_Value[i];
        }
        Destroy(Inven_slot_obj);
    }

    public void Craft_Click() 
    {
        for (int x = 0; x < itemForCraft_code.Count; x++)
        {
            Debug.LogError("x = " + itemForCraft_code[x] + ", i = " + itemForCraft_Value[x]);
            createinven.Remove_Item(itemForCraft_code[x], itemForCraft_Value[x]);
        }
        createinven.Add_Item(CodeItem, 1);
    }
}
