using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class monster_hited_damaged : MonoBehaviour
{
    public float movespeed,alphaSpeed;
    public TextMeshPro Figure_text;
    Color alpha;
    // Start is called before the first frame update
    void Start()
    {
        Figure_text = GetComponent<TextMeshPro>();
        alpha = Figure_text.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, movespeed * Time.deltaTime,0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        Figure_text.color = alpha;
    }
   
    public void apply_figure(Transform input_transform,int input_figure)
    {
        alpha.a = 255;
        gameObject.transform.position = input_transform.position;
        Figure_text.text = input_figure.ToString();
        Invoke("de_activate", 2f);
    }
    public void de_activate()
    {
        gameObject.SetActive(false);
    }
}
