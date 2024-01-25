using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnSlotCountChange();
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public int slotCnt;//

    public List<Item> items = new List<Item>();
    public GameObject player;
    public Player_Status status;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        status = player.GetComponent<Player_Status>();
        onSlotCountChange.Invoke();

    }


    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke();//µ¨¸®°ÔÀÌÆ® È£Ãâ
        }
    }

    public bool AddItem(Item _item)
    {
        if (status.pocket_money >= _item.price)
        {
            if (items.Count < slotCnt)
            {
                status.pocket_money -= _item.price;
                items.Add(_item);
                if (onChangeItem != null)
                    onChangeItem.Invoke();
                return true;
            }
        }
            Debug.Log("¾ÆÀÌÅÛ È×µæ ½ÇÆÐ");
            return false;
        
        
        
    }

    public void RemoveItem(int index)
    {
        items.RemoveAt(index);
        onChangeItem.Invoke();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FieldItem"))
        {
            Debug.Log("¾ÆÀÌÅÛ È×µæ ");
            FieldItems fieldItems = collision.gameObject.GetComponent<FieldItems>();
            if (AddItem(fieldItems.GetItem()))
            {
                fieldItems.DestroyItem();
            }


        }


    }
}
