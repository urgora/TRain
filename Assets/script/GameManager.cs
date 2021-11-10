using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource Audio,bgs;
    public AudioClip win, loss, welcome, mascotintro,gameintro,winmusic;
    public GameObject wincanvas,moscot,panel,winpanel,menubox,stars,player,circle;
    
    public bool gamestarted;
   
    private void Start()
    {
        gamestarted = false;
       
        Screen.sleepTimeout = SleepTimeout.NeverSleep;


        StartCoroutine(intro());
    }


    IEnumerator intro()
    {
      
        yield return new WaitForSeconds(.5f);
        moscot.GetComponent<Animator>().enabled = true;
        Audio.clip=mascotintro;
        Audio.Play();
        yield return new WaitForSeconds(mascotintro.length);
        Audio.PlayOneShot(welcome);
        yield return new WaitForSeconds(welcome.length);
        Audio.PlayOneShot(gameintro);
        yield return new WaitForSeconds(gameintro.length);
        moscot.GetComponent<Animator>().enabled = false;
        moscot.transform.DOMove(circle.transform.position, 1);  
        moscot.transform.DOScale(new Vector3(.37f,.37f,.37f), 1) 
            .OnComplete(() => {  
                Debug.Log("Done");
                panel.SetActive(false);
                
                bgs.Play();
                gamestarted = true;
                player.SetActive(true);
               
            });
    }

    public void skip()
    {
        Audio.Stop();
      StopAllCoroutines();
        moscot.GetComponent<Animator>().enabled = false;
        moscot.transform.DOMove(circle.transform.position, 0.1f);  
        moscot.transform.DOScale(new Vector3(.37f,.37f,.37f), 0.1f) 
            .OnComplete(() => {  
                Debug.Log("Done");
                panel.SetActive(false);
                
                bgs.Play();
                gamestarted = true;
                player.SetActive(true);
               
            });
    }
    public void WinAudio()
    {
        Audio.PlayOneShot(win);
       
    }  
    public void LevelDone()
    {
        bgs.Stop();
        moscot.SetActive(false);

                StartCoroutine(winefect());
    }

    public void losss()
    {
        Audio.PlayOneShot(loss);
    }

    public int noofwrong;
  
    IEnumerator winefect()
    {
     
        Audio.PlayOneShot(winmusic);
        
        yield return new WaitForSeconds(.5f);
        
        moscot.GetComponent<SpriteRenderer>().sortingOrder = 19;
        winpanel.SetActive(true);

        if (noofwrong<=1)
        {
            stars.GetComponent<starFxController>().ea = 3;
            stars.SetActive(true);
        }
        else if (noofwrong<=3)
        {
            stars.GetComponent<starFxController>().ea = 2;
            stars.SetActive(true);
        }
        else if (noofwrong<=6)
        {
            stars.GetComponent<starFxController>().ea = 1;
            stars.SetActive(true);
        }
        
        yield return new WaitForSeconds(winmusic.length);
       
        menubox.SetActive(true);
       
    }
 
  
    public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void quit()
    {
        Application.Quit();
    }

   

   
  
}
