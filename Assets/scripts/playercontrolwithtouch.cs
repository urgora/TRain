using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playercontrolwithtouch : MonoBehaviour
{

    Touch touch;
    GameObject objtodrag;
    Vector2 position;
    public float speed;
    public Rigidbody2D rb;
    public bool selected;
    public Animator anim;

    public GameManager gm;
    
    private void FixedUpdate()
    {
        if (selected)
        {
            rb.MovePosition(position);
        }

    }
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    selected = true;
                }
            }
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touch = Input.GetTouch(0);
            Vector3 touchpos = Camera.main.ScreenToWorldPoint(touch.position);
            position = Vector2.Lerp(transform.position, touchpos, speed * Time.deltaTime);
            if (selected)
            {
                Vector3 direction = transform.position - touchpos;
                float angle2 = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
               // rb.rotation = angle2+180;
            }

        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            selected = false;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 4)
        {
            anim.SetTrigger("Shake");
            gm.noofwrong++;
            gm.losss();
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.tag=="Finish")
        {
            Debug.Log("finish");
            gm.WinAudio();
            Invoke("gameover", .1f);
        }
    }
    public void reload()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
    public void gameover()
    {
        gm.LevelDone();
    }
}
