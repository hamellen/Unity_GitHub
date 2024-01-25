using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionDatabase : MonoBehaviour
{
    public static ConditionDatabase instance_condition;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance_condition == null)
        {
            instance_condition = this;
            
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static ConditionDatabase Instance
    {
        get
        {
            if (null == instance_condition)
            {
                return null;
            }
            return instance_condition;
        }
    }
    public List<Condition_icon> conditionDB = new List<Condition_icon>();
    public List<icon_index> indexDB = new List<icon_index>();
    // Update is called once per frame
    void Update()
    {
        
    }
}
