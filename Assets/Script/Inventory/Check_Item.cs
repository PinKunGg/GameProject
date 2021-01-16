using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Item : MonoBehaviour
{
    [SerializeField] private string[] all_item_code/* = new string[3] { "#000-00", "#000-01","#000-02"}*/;
    [SerializeField] string[][] all_item_Craft;
    [SerializeField] int[] all_item_value;
    [SerializeField] private Sprite[] all_pic_item;
    [SerializeField] List<CraftingItemData> craftingItem;

    public int getitemcraft_Lenght(string code) 
    {
        for (int x = 0; x < all_item_code.Length; x++)
        {
            if (code == all_item_code[x])
            {
                return all_item_Craft[x].Length;
            }
        }
        return 0;
    }
    public int getitemcode_Length()
    {
        return all_item_code.Length;
    }
    public string getitemcode(int value) 
    {
        return all_item_code[value];
    }
    public int getitemvalue(int value)
    {
        return all_item_value[value];
    }

    public Sprite getpic(int pos) 
    {
        return all_pic_item[pos];
    }

    void Start()
    {
        all_item_code = new string[craftingItem.Count];
        all_item_Craft = new string[craftingItem.Count][];
        all_pic_item = new Sprite[craftingItem.Count];
        for (int x = 0; x < craftingItem.Count; x++) 
        {
            all_item_code[x] = craftingItem[x].code_item;
            all_pic_item[x] = craftingItem[x].pic;
            all_item_Craft[x] = new string[craftingItem[x].all_item_Craft.Length];
            for (int i = 0; i < craftingItem[x].all_item_Craft.Length;i++) 
            {
                all_item_Craft[x][i] = craftingItem[x].all_item_Craft[i];
            }
        }
        all_item_value = new int[all_item_code.Length];
        //all_item_Craft[0] = new string[4] { "#201-01", "#201-02","5","3" };
        //all_item_Craft[1] = new string[6] { "#201-03", "#201-04","#201-05","1","2","3"};
    }

    void Update()
    {
        
    }

    public void Update_Item(string code,int value,int AllValue,bool first_Check) 
    {
        if (first_Check == true)
        {
            for (int i = 0; i < all_item_code.Length; i++)
            {
                if (code == all_item_code[i])
                {
                    all_item_value[i] += AllValue;
                }
            }
        }
        else 
        {
            for (int i = 0; i < all_item_code.Length; i++)
            {
                if (code == all_item_code[i])
                {
                    all_item_value[i] += value;
                }
            }
        }
    }
    public string CheckCraft(string code,int pos) 
    {
        for (int x = 0; x < all_item_code.Length; x++) 
        {
            if (code == all_item_code[x])
            {
                return all_item_Craft[x][pos];
            }
        }
        return null;
    }
}
