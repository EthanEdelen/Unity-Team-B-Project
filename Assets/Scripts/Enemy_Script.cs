using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    public float move_speed = 10.0f;
    public float jump_impulse = 10000.0f;
    public float rotation = 0.0f;
    public float rotate_speed = 100.0f;
    public bool is_grounded = true;
    public float jump_timer = 1.0f;
    public float jump_time_elapsed = 0.0f;
    public int difficulty_value = 1;
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
       
    }

    public void Movement()
    {
        Rigidbody my_rbody;
        my_rbody = GetComponent<Rigidbody>();
        Vector3 move_vec = Vector3.forward * move_speed * Time.fixedDeltaTime;
        my_rbody.AddRelativeForce(move_vec, ForceMode.Impulse);
        //my_rbody.velocity = move_vec;
    }

    public void Rotation()
    {
        GameObject player_object = GameObject.FindGameObjectWithTag("Player");
        if (player_object != null)
        {
            Player_Script player = player_object.GetComponent<Player_Script>();
            if (player != null)
            {
                // The step size is equal to speed times frame time.
                // Determine which direction to rotate towards
                Vector3 targetDirection = player.transform.position - transform.position;

                // The step size is equal to speed times frame time.
                float singleStep = rotate_speed * Time.deltaTime;

                // Rotate the forward vector towards the target direction by one step
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

                // Calculate a rotation a step closer to the target and applies rotation to this object
                transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);
            }
        }
    }

    public void Fix_Rotation()
    {
        if (this.transform.eulerAngles.y >= 315)
        {
            rotation = 315;
        }
        else if (this.transform.eulerAngles.y >= 270 && this.transform.eulerAngles.y < 315)
        {
            rotation = 270;
        }
        else if (this.transform.eulerAngles.y >= 225 && this.transform.eulerAngles.y < 270)
        {
            rotation = 225;
        }
        else if (this.transform.eulerAngles.y >= 180 && this.transform.eulerAngles.y < 225)
        {
            rotation = 180;
        }
        else if (this.transform.eulerAngles.y >= 0 && this.transform.eulerAngles.y < 45)
        {
            rotation = 0;
        }
        else if (this.transform.eulerAngles.y >= 45 && this.transform.eulerAngles.y < 90)
        {
            rotation = 45;
        }
        else if (this.transform.eulerAngles.y >= 90 && this.transform.eulerAngles.y < 135)
        {
            rotation = 90;
        }
        else if (this.transform.eulerAngles.y >= 135 && this.transform.eulerAngles.y < 180)
        {
            rotation = 135;
        }
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, rotation, this.transform.eulerAngles.z);
    }

    public void Jumping()
    {
        if (jump_time_elapsed >= jump_timer && is_grounded)
        {
            Vector3 jump_vec = Vector3.up * jump_impulse * Time.fixedDeltaTime;
            Rigidbody my_rbody;
            my_rbody = GetComponent<Rigidbody>();
            my_rbody.AddRelativeForce(jump_vec, ForceMode.Impulse);
            jump_time_elapsed = 0;
            print(jump_time_elapsed);

        }
    }

    public bool CheckGrounded()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, this.GetComponent<Collider>().bounds.extents.y + 0.1f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AdjustFriction()
    {
        if (is_grounded)
        {
            this.GetComponent<Collider>().material.dynamicFriction = 0.6f;
            this.GetComponent<Collider>().material.staticFriction = 0.6f;
        }
        else
        {
            this.GetComponent<Collider>().material.dynamicFriction = 0.0f;
            this.GetComponent<Collider>().material.staticFriction = 0.0f;
        }
    }

    public int GetDifficulty()
    {
        return difficulty_value;
    }
}
