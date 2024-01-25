using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.EventSystems;

public class puls_slot : MonoBehaviour, IPointerUpHandler
{

    public GameObject manager;
    public InventoryUI inven_ui;
    
    // Start is called before the first frame update
    void Awake()
    {
        manager = GameObject.FindWithTag("GameManager");
        inven_ui = manager.GetComponent<InventoryUI>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        inven_ui.AddSlot();

    }
}
