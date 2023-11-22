using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public GameObject fish;
    public GameObject transformPoint;
    public Rigidbody rb;
    public GameObject cat;
    public Animator animator;
    public bool fallbool,runbool,loopstop = false;
    public bool followbool, find,do_know = false;
   
    float speed = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        animator.GetComponent<Animator>();
    }


    void Update()
    {
        fa();
        if (fallbool == true && loopstop ==false)
        {
            animator.SetBool("Fall", true);
            fallbool = false;
            followbool = true;


        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("fish"))
        {
            Debug.Log("fish_catch");
            fish = other.gameObject;
            rb = fish.GetComponent<Rigidbody>();
            rb.useGravity = false;
            fish.transform.SetParent(transformPoint.transform);
            fish.transform.position = transformPoint.transform.position;
            fish.transform.rotation = transformPoint.transform.rotation;
            StartCoroutine(rotate());
           
        }

    }
    IEnumerator rotate()
    {
        yield return new WaitForSeconds(2);
        cat.transform.Rotate(0, 150f, 0);
        fallbool = true;
        


    }
    public void fa()
    {
        if (fallbool == false && followbool == true && do_know==false)
        {
            Debug.Log("h");
            animator.SetBool("Fall", false);
            loopstop = true;
            StartCoroutine(ro());

        }
    }
    IEnumerator ro()
    {
        yield return new WaitForSeconds(3);
        find = true;
        animator.SetBool("Run", true);
       
    }
}
