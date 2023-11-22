using GLTF.Schema;
using Oculus.Interaction;
using OVR;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class Cat : MonoBehaviour
{
    [SerializeField] float Food_distance = 2;
    [SerializeField] float foodCheckDistance= 1;
    [SerializeField] Collider[] _collider= new Collider[2];
    [SerializeField] Collider[] Collider;
    [SerializeField] Animator animator;
    [SerializeField] public GameObject Bowlpoint,milk,Sleeplocation,fishpoint,cat,co;
    [SerializeField] float speed = 0.5f;
    public LayerMask layer;
    public Ground Target;
    float distance;
    public bool issleep,rat_timebool;
    bool endbool = false;
    public bool iseatpoint,onbool = true;
    public float time=0f;
    public string newTag;
    public GameObject tagchange;
    public Fish F_script;
    public Rat Rat_script;
    public GameObject point,tongue,standpoint;
  
    void Start()
    {
        animator = GetComponent<Animator>();
       gameObject.tag= "bowl";
    }


    void Update()
    {
      
        On();
        fa();
        fish();
        catAnimation();
        foodAnimation();
       

    }
    void catAnimation()
    {

        _collider = Physics.OverlapSphere(transform.position, foodCheckDistance, layer, QueryTriggerInteraction.Collide);

        if (Target == null)
        {
            Debug.Log("bowl");

            foreach (Collider collider in _collider)
            {
                if (collider.gameObject != gameObject && collider.CompareTag("bowl")) 
                {
                    Ground chickenbowlScript = collider.GetComponent<Ground>();
                    Target = chickenbowlScript;
                    Debug.Log("name" + Target.name);
                    Debug.Log("enter");
                   

                }
                
                if (collider.gameObject.tag == "bowl")
                {
                    Debug.Log(collider.gameObject.name);
                    Bowlpoint = collider.gameObject;
                   transform.LookAt(Bowlpoint.transform.position);
                    Debug.Log("enter1");
                }
            }

            
        }
       
    }
    void foodAnimation()
    {
        distance = Vector3.Distance(transform.position, Bowlpoint.transform.position);
         if (distance < Food_distance  && Target._Ground)

         {
            Debug.Log("come");

            if (distance > 0.4f)
            {
                animator.SetBool("Walk", true);
                
                transform.position = Vector3.MoveTowards(transform.position, Bowlpoint.transform.position, speed * Time.deltaTime);


            }
           
            else
            {
               

                Collider = Physics.OverlapSphere(transform.position, 0.6f, layer);
                foreach(Collider collider in Collider)
                {
                    

                  if (collider.gameObject.CompareTag("Milk"))
                    {
                      

                        milk = collider.gameObject;
                        if(milk != null)
                        {
                           cat. transform.position = standpoint.transform.position;
                            transform.LookAt(Bowlpoint.transform.position);

                            animator.SetBool("Walk", false);
                           
                            animator.SetBool("Drink", true);
                            StartCoroutine(Drink());
                        }
                    }
                }
               
              
                
            }
         }
        if (issleep==true)
        {
            tagchange.tag = "Untagged";
            tagchange.tag = newTag;
             co.GetComponent<MeshCollider>().enabled = false;
           // tagchange.SetActive(false);
            animator.SetBool("Walk", true);
            transform.LookAt(Sleeplocation.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, Sleeplocation.transform.position, speed * Time.deltaTime);
           
            Collider = Physics.OverlapSphere(transform.position, 0.6f, layer);


            foreach (Collider collider in Collider)
            {
                if (collider.gameObject.CompareTag("Sleep"))
                {
                    transform.position = Sleeplocation.transform.position;
                    animator.SetBool("Walk", false);
                    animator.SetBool("Lie", true);
                    time = 0;
                    Rat_script.time = 0;


                }
            }
        }
    }
    IEnumerator Drink()
    {
        yield return new WaitForSeconds(10);
        animator.SetBool("Drink", false);
        milk.SetActive(false);
        issleep = true;

    }
    void fish()
    {
    
        time += 1 * Time.deltaTime;
        if (time >= 30f && iseatpoint)
        {
            //issleep = false;
            time = 30f;
           
          
            
            Debug.Log("time");
            animator.SetBool("Lie", false);
            animator.SetBool("Trot", true);
            transform.LookAt(fishpoint.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, fishpoint.transform.position, speed * Time.deltaTime);
            Debug.Log("s");
        }
        


        if (fishpoint.transform.position==cat.transform.position)
        {
               
                animator.SetBool("Trot", false);
                animator.SetBool("jump", true);
                iseatpoint = false;
         }
        else
        {
            animator.SetBool("jump", false);
           
        }
          




        
    }
  
    public  void fa()
    {
        if (F_script.find == true)
        {
            Debug.Log("o");
            transform.LookAt(point.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, point.transform.position, speed * Time.deltaTime);
          
            StartCoroutine(nul());
          
        }
        
    }
    IEnumerator nul()
    {
       
        F_script.do_know = true;
        F_script.fallbool = false;
        animator.SetBool("Run", false);
        yield return new WaitForSeconds(5);
      
       
       
        onbool = false;

    }
    void On()
    {
        if (onbool == false)
        {
            animator.SetBool("Eatloop", true);
            F_script.fish.transform.SetParent(null);
            F_script.rb.useGravity = true;
            Destroy(F_script.fish, 2);

           // onbool = true;
            Debug.Log("eeeee");
            endbool = true;
            StartCoroutine(end());
        }
        
    }
  
    IEnumerator end()
    {
        yield return new WaitForSeconds(5);
       
        animator.SetBool("nick", true);
        rat_timebool = true;
        Debug.Log("end");
        animator.SetBool("Eatloop", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Food_distance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, foodCheckDistance);

    }
}
