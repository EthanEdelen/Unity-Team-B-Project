using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle_Script : Enemy_Script
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //Set in Prefab?
        //move_speed = 8.0f;
        //rotation = 0.0f;
        //rotate_speed = 100.0f;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        jump_time_elapsed += Time.deltaTime;
    }

    public override void FixedUpdate()
    {
        is_grounded = CheckGrounded();
        Movement();
        base.FixedUpdate();
    }
}
