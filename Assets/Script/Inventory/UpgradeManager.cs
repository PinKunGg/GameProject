using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public List<int> eiei = new List<int>();
    [SerializeField]private GameObject UpgradeScrollCon, UpgradeDefaultItem ,UpgradeItem;
    [SerializeField]private RectTransform ScrollConArea, UpgradeDefaultItemRecTrans;
    private GameObject UpgradeList;
    private float CanvasHight = 197f;
    private int UpgradeCount;

    private void Awake()
    {
        eiei.Add(1);
        eiei.Add(2);
        eiei.Add(3);
        eiei.Add(4);
        eiei.Add(5);
    }
    private void Start()
    {
        UpgradeDefaultItem.SetActive(false);

        for(int x = 0; x < eiei.Count; x++)
        {
            ScrollConArea.sizeDelta = new Vector2(1100, CanvasHight);
            CanvasHight += 197f;
        }

        //foreach(var item in UpgradeContainer)
        //{
        //    item.UpgradeSCAO.LoadDataFromSave();
            
        //    if(item.UpgradeSCAO.UpgradeIndex != 0 && item.UpgradeSCAO.isRemove == false)
        //    {
        //        UpgradeList = Instantiate(UpgradeItem, UpgradeDefaultItemRecTrans.position, Quaternion.identity);
        //        UpgradeDefaultItemRecTrans.localPosition = new Vector3(UpgradeDefaultItemRecTrans.localPosition.x + 100f,UpgradeDefaultItemRecTrans.localPosition.y,UpgradeDefaultItemRecTrans.localPosition.z);
        //        UpgradeList.transform.parent = UpgradeScrollCon.transform;
        //        UpgradeList.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
        //        UpgradeList.SetActive(true);
        //        UpgradeList.GetComponent<UpgradeItem>().SetInfo(item.UpgradeSCAO.UpgradeIndex, item.UpgradeSCAO.UpgradeCost,item.UpgradeSCAO.UpgradeIcon);
        //        UpgradeCount++;
        //    }
        //}
    }
}
