using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alligator_Script : Enemy_Script
{
    // Start is called before the first frame update
    void Start()
    {
        move_speed = 8.0f;
        rotation = 0.0f;
        rotate_speed = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void FixedUpdate()
    {
        Movement();
        //FlyMovement();
        Rotation();
        Fix_Rotation();
        CheckLife();
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
