using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEft/Consumable/Speed")]
public class ItemQuickEft : ItemEffect
{

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override bool ExecuteRole()
    {
        ItemDatabase.instance.activate_effect(2);

        return true;
    }
}
