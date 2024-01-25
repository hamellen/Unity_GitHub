using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumable/Fixed")]
public class ItemFixedEft : ItemEffect
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override bool ExecuteRole()
    {
        ItemDatabase.instance.activate_effect(7);

        return true;
    }
}
