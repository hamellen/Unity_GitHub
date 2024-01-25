using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ItemEft/Consumable/movespeed")]
public class ItemAgilityEft : ItemEffect
{

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override bool ExecuteRole()
    {
        ItemDatabase.instance.activate_effect(4);

        return true;
    }
}
