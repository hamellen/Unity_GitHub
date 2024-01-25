using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltpControl_inventory : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

    
    public Tooltip tooltip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Item item = GetComponent<Slot>().item;
        
        if (item != null)
        {
            tooltip.gameObject.SetActive(true);
            tooltip.SetupTooltip(item.itemname, item.item_description);
        }
        Invoke("Set_false_Enter", 2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }

    public void Set_false_Enter()
    {
        tooltip.gameObject.SetActive(false);
    }
}
