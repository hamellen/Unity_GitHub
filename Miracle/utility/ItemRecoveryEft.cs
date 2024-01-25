using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEft/Consumable/recovery")]
public class ItemRecoveryEft : ItemEffect
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override bool ExecuteRole()
    {
        ItemDatabase.instance.activate_effect(6);

        return true;
    }
}
