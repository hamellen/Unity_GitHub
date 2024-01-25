using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{


    public Player_Status status;
    public Inventory inven;
    public GameObject inventoryPanel;
    public bool activeInventory = false,active_record=false;
    public GameObject shop;
    public Button closeShop;
    public bool isStoreActive;
    public Slot[] slots;
    public permanent_slot[] permanent_slots;
    public Transform slotHoler,permanent_holder;
    public GameObject player_gained_coin_object;
    public Text player_gained_coin;
    public GameObject player;
    public GameObject Record_book;
   

    public int slot_activate_fee;//추가 슬롯 활성화 비용

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        status = player.GetComponent<Player_Status>();
        inven = player.GetComponent<Inventory>();
        slots = slotHoler.GetComponentsInChildren<Slot>();
        permanent_slots= permanent_holder.GetComponentsInChildren<permanent_slot>();
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        inventoryPanel.SetActive(activeInventory);
        closeShop.onClick.AddListener(DeActiveShop);//이벤트 함수 연결
        Record_book.SetActive(active_record);
    }
    
    
    void SlotChange()
    {
        

        for (int i = 0; i < slots.Length; i++) {

            slots[i].slotnum = i;

            if (i < inven.slotCnt)
            {
                slots[i].GetComponent<Button>().interactable = true;//슬롯 활성화 
                Debug.Log(i+"슬롯 활성화");
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = false;//슬롯 비활성화
                Debug.Log(i+"슬롯 비활성화");
            }
        }

    }

    

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.I)&&!isStoreActive)//상점이 열려있을시 인벤토리 단독 작용 불가능
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
        if (Input.GetMouseButtonDown(0))
        {
            RayShop();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            active_record = !active_record;
            Record_book.SetActive(active_record);
        }
    }

    public void AddSlot()//상점기능에 추가 
    {

        if(status.pocket_money>= slot_activate_fee)
        {
            if (inven.SlotCnt < slots.Length)
            {
                inven.SlotCnt++;
                status.pocket_money
                    
                    
                    -= slot_activate_fee;
            }
            else if (inven.SlotCnt == slots.Length)
            {
                Debug.Log("모든 인벤토리가 활성화되었습니다.");
            }
        }
        else if(status.pocket_money < slot_activate_fee)
        {
            Debug.Log("금액이 모자랍니다.");
        }

        
    }

    void RedrawSlotUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }

    }

    

    public void RayShop()
    {
        Vector3 mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -10;
        RaycastHit2D hit2d = Physics2D.Raycast(mousePos, transform.forward, 30);
        if (hit2d.collider != null)
        {
            if (hit2d.collider.CompareTag("Store"))
            {
                if (!isStoreActive)
                {
                    ActiveShop(true);
                }
                
            }
        }
    }

    public void ActiveShop(bool isOpen)
    {
        if (!activeInventory)
        {
            activeInventory = isOpen;
            isStoreActive = isOpen;
            shop.SetActive(isOpen);
            inventoryPanel.SetActive(isOpen);
        }
    }
    public void DeActiveShop()
    {
        isStoreActive = false;
        shop.SetActive(false);
        activeInventory = false;
        inventoryPanel.SetActive(activeInventory);
    }
}
