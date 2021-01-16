using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftManage : MonoBehaviour
{
    [SerializeField] RectTransform ScrollConArea;
    List<string> CraftList = new List<string>();
    GameObject Inven_slot_obj;
    [SerializeField] Transform Prefab_Inven_slot;
    GameObject[] craftSlot;
    private float CanvasHight = 197f;
    void Start()
    {
        CraftList.Add("#201-04");
        CraftList.Add("#200-08");
        CraftList.Add("#200-12");
        Fix_Size_Craft();
    }

    public void Fix_Size_Craft() 
    {
        for (int x = 0; x < CraftList.Count; x++)
        {
            ScrollConArea.sizeDelta = new Vector2(1100, CanvasHight);
            CanvasHight += 197f;
        }
        craftSlot = new GameObject[CraftList.Count];
        First_Create_Craft();
    }

    private void First_Create_Craft()  //สร้างช่องเก็บของ
    {
        Inven_slot_obj = Prefab_Inven_slot.GetChild(0).gameObject;
        for (int i = 0; i < craftSlot.Length; i++) //สร้างช่องทั่วไป
        {
            craftSlot[i] = Instantiate(Inven_slot_obj, Prefab_Inven_slot);
            craftSlot[i].GetComponent<CraftSlot>().SetCodeItem = CraftList[i];
        }
        Destroy(Inven_slot_obj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
