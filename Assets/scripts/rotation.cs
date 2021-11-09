using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    public GameObject train;
    GameObject nextpoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==6)
        {
            nextpoint = collision.gameObject;
        }
    }
    private void Update()
    {
        if(nextpoint!=null)
        train.transform.rotation = Quaternion.Lerp(train.transform.rotation, nextpoint.gameObject.transform.rotation, 1f);
    }

}
