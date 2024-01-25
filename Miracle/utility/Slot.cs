using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

 public class Slot : MonoBehaviour,IPointerUpHandler
{
    public int slotnum; 
    public Item item;
    public Image itemIcon;

    GameObject player;
    
    

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }
    
    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        player = GameObject.FindWithTag("Player");

        if (item != null)
        {
            bool isUse = item.Use();
            if (isUse)
            {
                player.GetComponent<Inventory>().RemoveItem(slotnum);
            }
        }
        
    }
}
