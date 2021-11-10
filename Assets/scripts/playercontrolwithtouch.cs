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
    public bool selected,cancontroll=true;
    public Animator anim;
    public bool finishmovement=false;

    public GameManager gm;
    public Transform finishposition;
    public CircleCollider2D one,two;
    public PolygonCollider2D three;
  
    private void FixedUpdate()
    {
        if (selected)
        {
            rb.MovePosition(position);
        }

    }
    private void Start()
    {
        
    }

    private void Update()
    {
        if(cancontroll)
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
        if(finishmovement)
        {
            movetodestini();
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
    void movetodestini()
    {
        transform.position = Vector3.MoveTowards(transform.position, finishposition.position, 0.01f);
        if (transform.position == finishposition.position)
        {
           
            finishmovement = false;
            transform.position = finishposition.position;
           // gameover();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            // three.enabled = false;
            Debug.Log("finish");
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            cancontroll = false;
            finishmovement = true;
           Invoke("gameover", 2.5f);
        }
    }
    public void reload()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
    public void gameover()
    {
        gm.WinAudio();
        gm.LevelDone();
    }
}
