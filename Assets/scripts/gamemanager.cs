using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{
    public List<GameObject> followpath= new List<GameObject>();
    public GameObject train,ontrack,crash,handremoved,finish;
    public float speed;
    public int curretposition;
    public bool canaddtolist,reverse,finishcheck;
    public Vector2 starposition;
    void Start()
    {
        canaddtolist = true;
        starposition = train.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
            if (Physics.Raycast(ray, out hit))
            {
               
                if (hit.transform.tag == "path"&& canaddtolist)
                {
                    if (!hit.collider.gameObject.GetComponent<path>().added)
                    {
                        GameObject temp = hit.transform.gameObject;
                        followpath.Add(temp);
                        hit.collider.gameObject.GetComponent<path>().added = true;
                    }
                    
                }
                if(hit.transform.tag=="block")
                {
                    canaddtolist = false;
                    
                }
                if (hit.transform.tag == "cheat")
                {
                    ontrack.SetActive(true);
                    Invoke("reload", 4f);
                }
                if (hit.transform.tag == "finish")
                {
                    finishcheck = true;
                }


            }
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            reverse = true;
        }
        if (!canaddtolist && curretposition == followpath.Count)
        {
            crash.SetActive(true);
            Invoke("reload", 4f);
        }
        if(curretposition==0&&reverse)
        {
            handremoved.SetActive(true);
            Invoke("reload", 4f);
        }
        if(finishcheck&&curretposition==followpath.Count)
        {
            finish.SetActive(true);
            Invoke("reload", 4f);
        }
    }
    private void FixedUpdate()
    {
        if(followpath.Count>0)
           train.transform.position = Vector2.MoveTowards(train.transform.position, followpath[curretposition].transform.position, speed);

        if (train.transform.position == followpath[curretposition].transform.position)
        {
            if (curretposition < followpath.Count)
            {
                if (reverse)
                    curretposition--;
                else
                    curretposition++;
            }
        }
       


        if(train.transform.position==followpath[0].transform.position&&reverse)
        {
            reverse = false;
            canaddtolist = true;
            followpath.Clear();
        }
    }
    public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
