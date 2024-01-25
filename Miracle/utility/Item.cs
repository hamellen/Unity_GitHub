using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumables,
    Etc
    
}



[System.Serializable]
public class Item  
{
    public ItemType itemtype;
    public string itemname;
    public string item_description;
    public Sprite itemImage;
    public ItemEffect eft;
    public int price;

    public bool Use()
    {
        bool isUsed = false;

        if (eft != null)
        {
            isUsed = eft.ExecuteRole();
        }
        
           
        return isUsed;
    }

}
