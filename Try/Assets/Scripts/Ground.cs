using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public bool _Ground;

    void Start()
    {
        
    }

   
    void Update()
    {
        groundcheck();  
    }
   void groundcheck()
    {
        if (transform.position.y <= 0.1)
        {
            _Ground = true;
        }
        else
        {
            _Ground = false;
        }
    }
}
