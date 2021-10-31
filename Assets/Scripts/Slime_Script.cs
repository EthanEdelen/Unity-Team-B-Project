using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Script : Enemy_Script
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        jump_time_elapsed += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        is_grounded = CheckGrounded();
        AdjustFriction();
        Movement();
        Rotation();
        Fix_Rotation();
        Jumping();
        CheckLife();
    }
}
