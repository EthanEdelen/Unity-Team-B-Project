using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alligator_Script : Enemy_Script
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //Set in Prefab?
        //move_speed = 8.0f;
        //rotation = 0.0f;
        //rotate_speed = 100.0f;
        if (special)
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.material.color = new Color(0.6f, 0.0f, 0.7f);
            renderer = transform.GetChild(0).GetComponent<MeshRenderer>();
            renderer.material.color = new Color(0.6f, 0.0f, 0.7f);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        jump_time_elapsed += Time.deltaTime;
    }

    public override void FixedUpdate()
    {
        float temp = move_speed;
        GameObject player_object = GM_Script.GM.playerObject;
        if (player_object != null)
        {
            Player_Script player = player_object.GetComponent<Player_Script>();
            if (player != null)
            {
                // The step size is equal to speed times frame time.
                // Determine which direction to rotate towards
                float distance = Vector3.Distance(player.transform.position, transform.position);
                if (distance <= 10 && distance >= 7)
                {
                    Jumping();
                }
                
            }
        }
        is_grounded = CheckGrounded();
        Movement();
        move_speed = temp;
        base.FixedUpdate();
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (special)
        {
            collision.gameObject.GetComponent<Player_Script>().Slow.Effect(collision.gameObject);
        }
    }


    //void Movement()
    //{
    //    Rigidbody my_rbody = GetComponent<Rigidbody>();
    //    Vector3 move_vec = Vector3.forward * move_speed * Time.fixedDeltaTime;
    //    my_rbody.AddRelativeForce(move_vec, ForceMode.Impulse);
    //}

    //void Rotation()
    //{
    //    GameObject player_object = GameObject.FindGameObjectWithTag("Player");
    //    if(player_object != null)
    //    {
    //        Player_Script player = player_object.GetComponent<Player_Script>();
    //        if (player != null)
    //        {
    //            // The step size is equal to speed times frame time.
    //            // Determine which direction to rotate towards
    //            Vector3 targetDirection = player.transform.position - transform.position;

    //            // The step size is equal to speed times frame time.
    //            float singleStep = rotate_speed * Time.deltaTime;

    //            // Rotate the forward vector towards the target direction by one step
    //            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

    //            // Calculate a rotation a step closer to the target and applies rotation to this object
    //            transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
    //        }
    //    }
    //}
    //void Fix_Rotation()
    //{
    //    if (this.transform.eulerAngles.y >= 315)
    //    {
    //        rotation = 315;
    //    }
    //    else if (this.transform.eulerAngles.y >= 270 && this.transform.eulerAngles.y < 315)
    //    {
    //        rotation = 270;
    //    }
    //    else if (this.transform.eulerAngles.y >= 225 && this.transform.eulerAngles.y < 270)
    //    {
    //        rotation = 225;
    //    }
    //    else if (this.transform.eulerAngles.y >= 180 && this.transform.eulerAngles.y < 225)
    //    {
    //        rotation = 180;
    //    }
    //    else if (this.transform.eulerAngles.y >= 0 && this.transform.eulerAngles.y < 45)
    //    {
    //        rotation = 0;
    //    }
    //    else if (this.transform.eulerAngles.y >= 45 && this.transform.eulerAngles.y < 90)
    //    {
    //        rotation = 45;
    //    }
    //    else if (this.transform.eulerAngles.y >= 90 && this.transform.eulerAngles.y < 135)
    //    {
    //        rotation = 90;
    //    }
    //    else if (this.transform.eulerAngles.y >= 135 && this.transform.eulerAngles.y < 180)
    //    {
    //        rotation = 135;
    //    }
    //}
}
