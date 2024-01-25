using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop_slot : MonoBehaviour, IPointerUpHandler
{
    public int shop_slotnum;
    public Item item;
    public Image itemIcon;
    public Inventory inventory;
    

    GameObject player;

    public void UpdateSlotUI()
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        
        item = ItemDatabase.instance.itemDB[shop_slotnum];
        UpdateSlotUI();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        player = GameObject.FindWithTag("Player");
        inventory = player.GetComponent<Inventory>();

        if (item != null)//상점에서의 아이템 구매 
        {
            inventory.AddItem(ItemDatabase.instance.itemDB[shop_slotnum]);//
            Debug.Log(shop_slotnum + "번째 아이템 구매");
        }

    }
}
