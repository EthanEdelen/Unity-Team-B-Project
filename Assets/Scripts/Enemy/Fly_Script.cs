using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_Script : Enemy_Script
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        FlyMovement();
        Rotation();
        Fix_Rotation();
        CheckLife();
    }
}
