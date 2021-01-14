using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateInventory : MonoBehaviour
{
    [SerializeField] string[] Inven_code_string = new string[32];
    [SerializeField] int[] Inven_value_int = new int[32];

    GameObject[] Inven_slot = new GameObject[32]; //ช่องเก็บของ
    [SerializeField] GameObject[] HotKey_Slot_Panel = new GameObject[8];
    GameObject Inven_slot_obj; //ตัวช่องที่จะสร้าง
    [SerializeField] Transform Prefab_Inven_slot; //ตัวต้นแบบที่จะสร้าง
    [SerializeField] GameObject Inventory_Panel;
    [SerializeField] GameObject HotKey_Panel;
    [SerializeField] HotKey hotKey_Script;
    [SerializeField] Check_Item check_item;

    int HotKeySet = 0;
    public GameObject select_slot;

    public string GetCode(int pos) 
    {
        return Inven_code_string[pos]; 
    }
    public int GetValue(int pos) 
    {
        return Inven_value_int[pos];
    }
    void Start()
    {
        //SaveAndLoadInvoke.SALIKinstanse.AddSavingEventLisener(SaveInvenData); //เก็บเอาไว้ก่อน
        //SaveAndLoadInvoke.SALIKinstanse.AddLoadingEventLisener(LoadInvenData);
        First_Create_Inven();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            Add_Item("#201-01", 1);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Add_Item("#201-02", 1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Remove_Item");
            Remove_Item("#410-01", 3);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Inventory_Panel.activeInHierarchy == false)
            {
                Inventory_Panel.SetActive(true);
                HotKey_Panel.SetActive(false);
            }
            else if (Inventory_Panel.activeInHierarchy == true) 
            {
                Inventory_Panel.SetActive(false);
                HotKey_Panel.SetActive(true);
            }
        }
    }
    public void SaveInvenData()
    {
        //JsonSaveSystem.JSInstanse.InventData.Inven_ItemCode = Inven_code_int;
        JsonSaveSystem.JSInstanse.InventData.Inven_Value = Inven_value_int;
        JsonSaveSystem.JSInstanse.InventSaveJson();
    }
    public void LoadInvenData()
    {
        JsonSaveSystem.JSInstanse.InventLoadJson();
            
        for(int i = 0; i < 32; i++)
        {
            //Inven_code_int[i] = JsonSaveSystem.JSInstanse.InventData.Inven_ItemCode[i];
            Inven_value_int[i] = JsonSaveSystem.JSInstanse.InventData.Inven_Value[i];
            Inven_slot[i].GetComponent<InvenSlot>().LoadItem();
        }

        LinkHotKey();
        Count_Item(check_item.getitemcode(0), 0,true);
    }

    private void First_Create_Inven()  //สร้างช่องเก็บของ
    {
        Inven_slot_obj = Prefab_Inven_slot.GetChild(0).gameObject;
        for (int i = 0; i < Inven_slot.Length; i++) //สร้างช่องทั่วไป
        {
            Inven_slot[i] = Instantiate(Inven_slot_obj, Prefab_Inven_slot);
            Inven_slot[i].GetComponent<InvenSlot>().Set_SlotNumber = i + 1;
            Inven_slot[i].GetComponent<InvenSlot>().Set_hotkey = false;
        }
        Destroy(Inven_slot_obj);
    }

    string Read_Code_Item(string code,int pos) 
    {
        char[] code_item_char = code.ToCharArray();
        return code_item_char[pos].ToString();
    }

    public void Add_Item(string CodeItem,int value) //เพิ่มไอเท็ม
    {
        for (int i = 0; i < Inven_slot.Length; i++) //เช็คว่าไอเท็มควรไปอยู่ช่องไหน
        {
            if (CodeItem == Inven_code_string[i] && Inven_value_int[i] < 10 && Read_Code_Item(CodeItem,3) == "1") //หาช่องที่มีไอเท็มนี้อยู่แล้วก่อน
            {
                //Inven_value_int[i]+=value;              
                //Inven_slot[i].GetComponent<InvenSlot>().LoadItem();
                //Count_Item(CodeItem, value, false);
                //break;
                if (Inven_value_int[i] + value > 10)
                {
                    value -= 10 - Inven_value_int[i];
                    Inven_value_int[i] += 10 - Inven_value_int[i];
                    Inven_code_string[i] = CodeItem;

                    Inven_slot[i].GetComponent<InvenSlot>().LoadItem();
                    Count_Item(CodeItem, value, false);
                }
                else
                {
                    Inven_value_int[i] += value;
                    Inven_slot[i].GetComponent<InvenSlot>().LoadItem();
                    Count_Item(CodeItem, value, false);
                    break;
                }
            }
            if (i + 1 == Inven_slot.Length)
            {
                for (int x = 0; x < Inven_code_string.Length; x++)
                {
                    if (Inven_code_string[x] == check_item.getitemcode(0) && Inven_value_int[x] < 10) //ถ้าไม่มีจะไปเก็บที่ช่องที่ว่างอันแรกสุด
                    {
                        if (Inven_value_int[x] + value > 10) 
                        {
                            value -= 10 - Inven_value_int[x];                          
                            Inven_value_int[x] += 10 - Inven_value_int[x];
                            Inven_code_string[x] = CodeItem;

                            Inven_slot[x].GetComponent<InvenSlot>().LoadItem();
                            Count_Item(CodeItem, value, false);
                            break;
                        }
                        else
                        {
                            Inven_code_string[x] = CodeItem;
                            Inven_value_int[x] += value;
                            Inven_slot[x].GetComponent<InvenSlot>().LoadItem();
                            Count_Item(CodeItem, value, false);
                            break;
                        }
                    }
                }
                break;
            }
        }
        LinkHotKey();
    }

    public void Remove_Item(string CodeItem, int value) //ใช้งานไอ็เท็ม
    {
        int value_for_check = value;
        for (int x = 0; x < check_item.getitemcode_Length() ; x++) 
        {
            if (CodeItem == check_item.getitemcode(x) && value <= check_item.getitemvalue(x)) 
            {
                for (int i = Inven_code_string.Length-1; i >= 0; i--) 
                {
                    if (CodeItem == Inven_code_string[i]) 
                    {
                        if (Inven_value_int[i] >= value)
                        {
                            Inven_value_int[i] -= value;
                            value = 0;                           
                            if (Inven_value_int[i] == 0) 
                            {
                                Inven_code_string[i] = check_item.getitemcode(0);
                            }
                            Inven_slot[i].GetComponent<InvenSlot>().LoadItem();
                        }
                        else if (Inven_value_int[i] < value && Inven_value_int[i] > 0) 
                        {
                            value -= Inven_value_int[i];
                            Inven_value_int[i] = 0;
                            Inven_code_string[i] = check_item.getitemcode(0);
                            Inven_slot[i].GetComponent<InvenSlot>().LoadItem();
                        }
                    }
                    if (value <= 0) 
                    {
                        break;
                    }
                }
                Count_Item(CodeItem, -value_for_check, false);
            }
            if (value <= 0)
            {
                break;
            }
        }
        LinkHotKey();
    }

    void Count_Item(string code,int value,bool First_Check) //นับจำนวนไอเท็มทั้งหมด
    {
        if (First_Check == true) 
        {
            for (int x = 0; x < Inven_code_string.Length; x++)
            {
                if (Inven_code_string[x] != check_item.getitemcode(0))
                {
                    check_item.Update_Item(Inven_code_string[x], value, Inven_value_int[x], First_Check);
                }
            }
        }
        else
        {
            check_item.Update_Item(code, value, 0, First_Check);
        }
    }

    public void Select_Slot(GameObject slot) //บอกว่าเลือกช่องไหนอยู่
    {
        select_slot = slot;
    }

    public void Check_tpye_for_swap(GameObject target_slot)
    {
        if (target_slot.GetComponent<InvenSlot>().Check_Equipment() == "#00x-xx")
        {
            Swap_Slot(target_slot);
        }
        else 
        {
            if (Read_Code_Item(select_slot.GetComponent<InvenSlot>().Set_CodeItem, 1) == target_slot.GetComponent<InvenSlot>().get_pos_Equipment(1))
            {
                if (Read_Code_Item(select_slot.GetComponent<InvenSlot>().Set_CodeItem, 2) == target_slot.GetComponent<InvenSlot>().get_pos_Equipment(2))
                {
                    Swap_Slot(target_slot);
                }
            }
        }
    }
    public void Swap_Slot(GameObject target_slot) //สลับข้อมูลของสองช่องที่เลือก
    {       
        if(target_slot.GetComponent<InvenSlot>().Set_CodeItem == select_slot.GetComponent<InvenSlot>().Set_CodeItem) //ถ้าเป็นไอเท็มชนิดเดียวกัน
        {
            if (Read_Code_Item(target_slot.GetComponent<InvenSlot>().Set_CodeItem, 3) == "1" || Read_Code_Item(select_slot.GetComponent<InvenSlot>().Set_CodeItem, 3) == "1")
            {
                if (target_slot.GetComponent<InvenSlot>().Set_Value == 10 || select_slot.GetComponent<InvenSlot>().Set_Value == 10) //ถ้ามีอันใดอันหนึ่งเต็ม 10
                {
                    string code = target_slot.GetComponent<InvenSlot>().Set_CodeItem;
                    int value = target_slot.GetComponent<InvenSlot>().Set_Value;
                    target_slot.GetComponent<InvenSlot>().Set_CodeItem = select_slot.GetComponent<InvenSlot>().Set_CodeItem;
                    target_slot.GetComponent<InvenSlot>().Set_Value = select_slot.GetComponent<InvenSlot>().Set_Value;
                    select_slot.GetComponent<InvenSlot>().Set_CodeItem = code;
                    select_slot.GetComponent<InvenSlot>().Set_Value = value;
                    select_slot = null;
                    LinkHotKey();
                }
                else if (target_slot.GetComponent<InvenSlot>().Set_Value + select_slot.GetComponent<InvenSlot>().Set_Value > 10) //ถ้าสองอันรวมกันแล้ว > 10
                {
                    if (select_slot.GetComponent<InvenSlot>().Set_SlotNumber > target_slot.GetComponent<InvenSlot>().Set_SlotNumber)
                    {
                        int value = 10 - target_slot.GetComponent<InvenSlot>().Set_Value;
                        target_slot.GetComponent<InvenSlot>().Set_Value = 10;
                        select_slot.GetComponent<InvenSlot>().Set_Value -= value;
                    }
                    else if (target_slot.GetComponent<InvenSlot>().Set_SlotNumber > select_slot.GetComponent<InvenSlot>().Set_SlotNumber)
                    {
                        int value = 10 - select_slot.GetComponent<InvenSlot>().Set_Value;
                        select_slot.GetComponent<InvenSlot>().Set_Value = 10;
                        target_slot.GetComponent<InvenSlot>().Set_Value -= value;
                    }
                }
                else if (target_slot.GetComponent<InvenSlot>().Set_Value + select_slot.GetComponent<InvenSlot>().Set_Value < 10) //ถ้าสองอันรวมกันแล้ว < 10
                {
                    if (select_slot.GetComponent<InvenSlot>().Set_SlotNumber > target_slot.GetComponent<InvenSlot>().Set_SlotNumber)
                    {
                        target_slot.GetComponent<InvenSlot>().Set_Value += select_slot.GetComponent<InvenSlot>().Set_Value;
                        select_slot.GetComponent<InvenSlot>().Set_Value = 0;
                    }
                    else if (target_slot.GetComponent<InvenSlot>().Set_SlotNumber > select_slot.GetComponent<InvenSlot>().Set_SlotNumber)
                    {
                        select_slot.GetComponent<InvenSlot>().Set_Value += target_slot.GetComponent<InvenSlot>().Set_Value;
                        target_slot.GetComponent<InvenSlot>().Set_Value = 0;
                    }
                }                
            }
            LinkHotKey();
        }
        else/* if (target_slot.GetComponent<InvenSlot>().Set_CodeItem != select_slot.GetComponent<InvenSlot>().Set_CodeItem)*/
        {
            string code = target_slot.GetComponent<InvenSlot>().Set_CodeItem;
            int value = target_slot.GetComponent<InvenSlot>().Set_Value;
            target_slot.GetComponent<InvenSlot>().Set_CodeItem = select_slot.GetComponent<InvenSlot>().Set_CodeItem;
            target_slot.GetComponent<InvenSlot>().Set_Value = select_slot.GetComponent<InvenSlot>().Set_Value;
            select_slot.GetComponent<InvenSlot>().Set_CodeItem = code;
            select_slot.GetComponent<InvenSlot>().Set_Value = value;
            select_slot = null;
            LinkHotKey();
        }
    }

    public void Switch_Hotkey() //สลับhotkey
    {
        if (HotKeySet == 0)
        {
            HotKeySet = 4;
        }
        else if (HotKeySet == 4) 
        {
            HotKeySet = 0;
        }
        LinkHotKey();
        hotKey_Script.UpdateWhenSwitchHotKey();
    }
    void LinkHotKey() //ส่งข้อมูลจาก 8 ช่องสุดท่ายให้ hotkey
    {
         for (int i = HotKeySet; i<HotKeySet+4;i++) 
         {
             HotKey_Slot_Panel[i].GetComponent<InvenSlot>().Set_CodeItem = Inven_slot[i+24].GetComponent<InvenSlot>().Set_CodeItem;
             HotKey_Slot_Panel[i].GetComponent<InvenSlot>().Set_Value = Inven_slot[i+24].GetComponent<InvenSlot>().Set_Value;
         }
        hotKey_Script.UpdateWhenSwitchHotKey();
        AddToSave();
    }
    void AddToSave() 
    {
        for (int x = 0; x<Inven_slot.Length ; x++) 
        {
            Inven_code_string[x] = Inven_slot[x].GetComponent<InvenSlot>().Set_CodeItem;
            Inven_value_int[x] = Inven_slot[x].GetComponent<InvenSlot>().Set_Value;
        }
    }
}
