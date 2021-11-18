using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : Character_Script
{
    //Jump cooldown?
    public float jump_timer = 1.0f;
    public float jump_time_elapsed = 0.0f;
    public int difficulty_value = 1;
    public bool aquatic = false;
    protected bool special = false;
    public float chance_to_be_special = 0.0f;

    public GameObject Coin;
    public Collectable_Manager manager;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (Random.Range(0.0f, 1.0f) < chance_to_be_special)
        {
            special = true;
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        //Movement();

        AdjustFriction();
        Rotation();
        Fix_Rotation();
        CheckLife();
    }

    public void CheckLife()
    {
        if (transform.position.y <= -10)
            Destroy(this.gameObject);
        if (transform.position.y <= 0 && !aquatic)
        {
            Destroy(this.gameObject);
        }
        if (transform.position.y <= 1 && aquatic)
        {
            float temp = jump_impulse;
            jump_impulse = 1500;
            Jumping();
            is_grounded = CheckGrounded();
            jump_impulse = temp;
        }
    }

    public override void Movement()
    {
        if (!lockMovement)
        {
            Rigidbody my_rbody;
            my_rbody = GetComponent<Rigidbody>();
            Vector3 move_vec = Vector3.forward * move_speed * Time.fixedDeltaTime;
            my_rbody.AddRelativeForce(move_vec, ForceMode.Impulse);
        }
    }

    public void Rotation()
    {
        

        //GameObject player_object = GameObject.FindGameObjectWithTag("Player");
        //Doesn't have to look through scene, communicates with the Game manager
        GameObject player_object = GM_Script.GM.playerObject;
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
        }
    }

    public int GetDifficulty()
    {
        return difficulty_value;
    }
    public override void Death()
    {
        if (alv)
        {
            GM_Script.GM.AddScore(score);
            manager.Spawning(transform.position, Coin);
            base.Death();
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (alv)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Character_Script>().GetHit(atk);
            }
        }
    }
}
