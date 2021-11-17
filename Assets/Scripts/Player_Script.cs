using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : Character_Script
{
    //Now in Char scrpit
    //protected bool is_alive = true;  !!! REPLACED WITH alv
    //protected float current_health;
    //protected float max_health = 100.0f;
    public Image ui_hp_bar_inner;
    //public float move_speed = 8000.0f;
    //public float jump_impulse = 1000000.0f;
    //public float rotation = 0.0f;
    //Inherited from character script^
    public GameObject projectile_Arrow;
    public GameObject projectile_Bomb;
    public float launchVelocity = 700f;

    private Vector3 start_position;

    private float up_axis = 0.0f;
    private float side_axis = 0.0f;

    //private bool is_grounded = true;
    //Inherited from character script^
    public Animator swordAnimator;

    private Rigidbody my_rbody;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        current_health = max_health;
        ui_hp_bar_inner.fillAmount = 1.0f;

        start_position = transform.position;

        my_rbody = GetComponent<Rigidbody>();

        /*swordHitbox = GameObject.Find("Sword Hitbox");
        swordHitbox.SetActive(false);*/
    }

    public override void Movement()
    {
        /// Velocity approach to movement
        my_rbody.velocity = new Vector3(up_axis * 15, my_rbody.velocity.y, -side_axis * 20);

        /// Force approach to movement
        /*if (up_axis != 0.0 | side_axis != 0.0)
        {
            Vector3 move_vec = Vector3.right * -move_speed * Time.fixedDeltaTime;
            my_rbody.AddRelativeForce(move_vec, ForceMode.Impulse);
        }*/
    }

    public void Rotation()
    {
        if (up_axis == 0.0 & side_axis == 0.0){}
        else
            rotation = Mathf.Rad2Deg * Mathf.Atan2(-side_axis, -up_axis);
        transform.eulerAngles = new Vector3(0, rotation, 0);

        /// Old method for snappy rotation. Saved in case above method runs
        /// into unforeseen issues
        /*else if (up_axis > 0.0 & side_axis == 0.0)
        {
            rotation = 180;
        }
        else if (up_axis > 0.0 & side_axis > 0.0)
        {
            rotation = 225;
        }
        else if (up_axis == 0.0 & side_axis > 0.0)
        {
            rotation = 270;
        }
        else if (up_axis < 0.0 & side_axis > 0.0)
        {
            rotation = 315;
        }
        else if (up_axis < 0.0 & side_axis == 0.0)
        {
            rotation = 0;
        }
        else if (up_axis < 0.0 & side_axis < 0.0)
        {
            rotation = 45;
        }
        else if (up_axis == 0.0 & side_axis < 0.0)
        {
            rotation = 90;
        }
        else if (up_axis > 0.0 & side_axis < 0.0)
        {
            rotation = 135;
        }*/
    }
    //Now in Char script
    //bool CheckGrounded()
    //{
    //    if(Physics.Raycast(transform.position, -Vector3.up, this.GetComponent<Collider>().bounds.extents.y + 0.1f))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    /// Checks if player has fallen off platform
    /// This method may be replaced with death plane collision
    void CheckFallen()
    {
        if( transform.position.y < 0)
        {
            GetHit(25.0f);

            /// Place player back on platform if alive
            if (alv)
                transform.position = start_position;
        }
    }

    void Jumping()
    {
        float jump_axis = Input.GetAxis("Jump");
        if (jump_axis != 0.0 & is_grounded)
        {
            Rigidbody my_rbody = GetComponent<Rigidbody>();
            Vector3 jump_vec = Vector3.up * jump_impulse * Time.fixedDeltaTime;
            my_rbody.AddRelativeForce(jump_vec, ForceMode.Impulse);
        }
    }

    void Fire_Proj()
    {
        GameObject Arrow = Instantiate(projectile_Arrow, transform.position + transform.right * -2, transform.rotation * projectile_Arrow.transform.rotation);  
        Arrow.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchVelocity / 2, launchVelocity / 2));
    }

    void Fire_Bomb()
    {
        GameObject Bomb = Instantiate(projectile_Bomb, transform.position + transform.right * -2, transform.rotation * projectile_Bomb.transform.rotation); 
        Bomb.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchVelocity / 2, launchVelocity / 2));
    }

    void Swing()
    {
        bool swing_axis = Input.GetButtonDown("Slash");
        if (swing_axis)
        {
            swordAnimator.SetTrigger("Swing");
        }
        /*if (is_swinging == false & swing_axis != 0.0f & slash_held == false)
        {
            swordHitbox.SetActive(true);
            swing_rotation = -90.0f;
            transform.GetChild(0).RotateAround(this.transform.position, Vector3.up, -90.0f);
            is_swinging = true;
        }
        if (is_swinging)
        {
            swing_rotation += 1500.0f * Time.fixedDeltaTime;
            transform.GetChild(0).RotateAround(this.transform.position, Vector3.up, 1500.0f * Time.deltaTime);
            if (swing_rotation >= 90.0f)
            {
                transform.GetChild(0).RotateAround(this.transform.position, Vector3.up, -swing_rotation);
                swordHitbox.SetActive(false);
                is_swinging = false;
            }
        }
        if (swing_axis != 0.0f & slash_held != true)
        {
            slash_held = true;
        }
        if (swing_axis == 0.0f & slash_held == true)
        {
            slash_held = false;
        }*/
    }

    /// Clamps axis to 1 or -1 if not equal to 0
    /// Allows for more-snappy movement/rotation
    float ClampAxis(float axis)
    {
        if (axis > 0)
            axis = 1;
        else if (axis < 0)
            axis = -1;
        return axis;
    }

    // Update is called once per frame
    public override void FixedUpdate() // This should be used for physics updates
    {
        base.FixedUpdate();
        if (alv)
        {
            is_grounded = CheckGrounded();
            AdjustFriction();
            Jumping();
            Movement();

        }
    }

    //Getters and Setters for testing purposes from here down
    

    public override void Update() //Any updates that need to bypass physics go here
    {
        base.Update();
        if (alv)
        {
            /// Fetch input and clamp it
            up_axis = ClampAxis(Input.GetAxis("Vertical"));
            side_axis = ClampAxis(Input.GetAxis("Horizontal"));

            Rotation();
            CheckFallen();
            Swing();
            if (Input.GetButtonDown("Projectile"))
            {
                Debug.Log("Fired projectile");
                //FireProjectile();  Replaced by character script function
                Fire_Proj();
            }
            if (Input.GetButtonDown("Bomb"))
            {
                Debug.Log("Fired Bomb");
                //FireProjectile();  Replaced by character script function
                Fire_Bomb();
            }
        }

        /// This can be added to another method as more UI functionality
        /// is added
        ui_hp_bar_inner.fillAmount = current_health * 0.01f;
    }
}
