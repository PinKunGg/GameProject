using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Crafting Data")]
public class CraftingItemData : ScriptableObject
{
    public string Name;
    public string code_item;
    public string[] all_item_Craft = new string[3];
    public Sprite pic;

}
