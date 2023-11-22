using GLTF.Schema;
using Oculus.Interaction;
using Oculus.Platform.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    public List<GameObject> Waypoints;
    public GameObject sphere,cat,Hunt_point,startingpoint;
    public float speed = 1;
    public float time = 0;
    int index = 0;
    public bool isLoop = true;
   public  Cat catscript;
   public Animator animator;
    public bool Lock;
    
    void Start()
    {
     
        animator = GameObject.Find("cat"). GetComponent<Animator>();
        catscript = FindObjectOfType<Cat>();
    }

   
    void Update()
    {
        if (catscript.rat_timebool == true)
        {
           
              Debug.Log("rat_timebool");
            
              time += Time.deltaTime;
            if (time >= 10)
            {
                Debug.Log("rat");
                time = 10;

                transform.LookAt(sphere.transform.position);
                Vector3 destination = Waypoints[index].transform.position;
                Vector3 newpos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

                transform.position = newpos;
                float distance = Vector3.Distance(transform.position, destination);
                if (distance <= 0.05)
                {
                    // if (index < Waypoints.Count - 1)
                    // {
                    index++;
                    // }
                    // else
                    // {
                    // if (isLoop)
                    //  {
                    // index = 0;
                    //  }
                    // }

                }

            }
        }
        if (cat.transform.position == startingpoint.transform.position)
        {
            animator.SetBool("Eatloop", false);
            animator.SetBool("nick", false);
            animator.SetBool("Run", true);
           cat.transform.LookAt(Hunt_point.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, Hunt_point.transform.position, speed * Time.deltaTime);
           
            StartCoroutine(run_false());
        }
        
    }
    IEnumerator run_false()
    {
        yield return new WaitForSeconds(9);
        animator.SetBool("Run", false);
        animator.SetBool("Eatloop", false);
        animator.SetBool("Attack", true);
        Lock = true;
        StartCoroutine(death_rat());
    }
    IEnumerator death_rat()
    {
        yield return new WaitForSeconds(5);
        animator.SetBool("Eatloop", false);
        animator.SetBool("Attack", false);
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("cube"))
        {
            Debug.Log("lock");
           
            cat.transform.position = startingpoint.transform.position;

        }
    }
}
