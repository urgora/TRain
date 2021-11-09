using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathmove : MonoBehaviour
{
    public List<GameObject> pathcheck = new List<GameObject>();
    Touch touch;
    public float touchspeed = 0.01f;
    public GameObject train;
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

           
            
                Vector2 touchpos = Camera.main.ScreenToWorldPoint(touch.position);
               

                if (touch.phase == TouchPhase.Moved)
                {
                    transform.position = new Vector3(touchpos.x , touchpos.y,0f);
                }
            
          
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("working");
        if(collision.gameObject.tag=="path")
        {
            pathcheck.Add(collision.gameObject);

        }
    }
    private void FixedUpdate()
    {
        train.transform.position = Vector2.MoveTowards(train.transform.position, pathcheck[pathcheck.Count - 1].transform.position, speed);
    }
}
