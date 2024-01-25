using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Tooltip : MonoBehaviour
{

    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI disription;

    public void SetupTooltip(string item_name,string item_discription)
    {
        transform.position = Input.mousePosition;
        nameTxt.text = item_name;
        disription.text = item_discription;
    }

    private void Update()
    {
       
    }
}
