using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroljoystick : MonoBehaviour
{
    public GameObject basejoy, handle;
    public Joystick joy;
    public Rigidbody2D rb;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    private void FixedUpdate()
    {
        Vector2 movepositionofplayer;
        movepositionofplayer.x = joy.Horizontal;
        movepositionofplayer.y = joy.Vertical;

        rb.MovePosition(rb.position + movepositionofplayer * speed);

        Vector3 direction = basejoy.transform.position - handle.transform.position;
        float angle2 = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      
        if(joy.Horizontal!=0 ||joy.Vertical!=0)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle2);
        }
    }
}
