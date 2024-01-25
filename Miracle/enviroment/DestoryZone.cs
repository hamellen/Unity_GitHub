using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("falling_rock"))
        {
            Destroy(collision.gameObject);
        }
    }
}
