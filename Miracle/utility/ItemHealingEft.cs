using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;


[CreateAssetMenu(menuName ="ItemEft/Consumable/Health")]
public class ItemHealingEft : ItemEffect
{

    

    public float healingpoint = 0;

    public void Start()
    {
        
    }

    public override bool ExecuteRole()
    {
        ItemDatabase.instance.Add_hp(healingpoint);
        return true;
    }

}
