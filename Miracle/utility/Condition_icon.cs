using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ConditionType
{
    strength, quick, solid, agility, focus, recovery, burn, weaken, deceleration, destruction, poison, coldair, cooling, fixed_attack
}

[System.Serializable]
public class Condition_icon { 
    public ConditionType conditiontype;
    public string conditionname;
    public string condition_description;
    public Sprite condition_Image;
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
