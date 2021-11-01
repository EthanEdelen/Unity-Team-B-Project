using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : MonoBehaviour
{
    protected bool is_alive = true;
    protected float current_health;
    protected float max_health = 100.0f;
    public Image ui_hp_bar_inner;

    private float attack_timer = .5f;
    public float Damage = 10.0f;
    public float move_speed = 8000.0f;
    public float jump_impulse = 1000000.0f;
    public float rotation = 0.0f;

    private Vector3 start_position;

    private float up_axis = 0.0f;
    private float side_axis = 0.0f;

    private bool is_grounded = true;
    /*private bool is_swinging = false;
    private bool slash_held = false;
    private float swing_rotation = -90.0f;
    private GameObject swordHitbox;*/
    public Animator swordAnimator;

    private Rigidbody my_rbody;


    // Start is called before the first frame update
    void Start()
    {
        current_health = max_health;
        ui_hp_bar_inner.fillAmount = 1.0f;

        start_position = transform.position;

        my_rbody = GetComponent<Rigidbody>();

        /*swordHitbox = GameObject.Find("Sword Hitbox");
        swordHitbox.SetActive(false);*/
    }

    void Movement()
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

    void Rotation()
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

    bool CheckGrounded()
    {
        if(Physics.Raycast(transform.position, -Vector3.up, this.GetComponent<Collider>().bounds.extents.y + 0.1f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// Checks if player has fallen off platform
    /// This method may be replaced with death plane collision
    void CheckFallen()
    {
        if( transform.position.y < 0)
        {
            TakeDamage(25.0f);

            /// Place player back on platform if alive
            if (is_alive)
                transform.position = start_position;
        }
    }

    void AdjustFriction()
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

    void TakeDamage(float amt)
    {
        current_health -= amt;
        if (current_health <= 0)
        {
            is_alive = false;
            up_axis = 0.0f;
            side_axis = 0.0f;
        }
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

    void FireProjectile()
    {
        //Quaternion brot = transform.rotation * Quaternion.AngleAxis(180, Vector3.up);
        //Instantiate(/*bullet prefab here*/, transform.position + new Vector3(-transform.up.x, 0, -transform.up.z) * 8, brot);
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
    void FixedUpdate() // This should be used for physics updates
    {
        if (is_alive)
        {
            is_grounded = CheckGrounded();
            AdjustFriction();
            Jumping();
            Movement();
            if (Input.GetButtonDown("Projectile"))
            {
                Debug.Log("Fired projectile");
                FireProjectile();
            }
        }
    }

    //Getters and Setters for testing purposes from here down
    public float GetCurrentHealth()
    {
        return current_health;
    }

    public float GetMaxHealth()
    {
        return max_health;
    }

    public void SetCurrentHealth(float newHealth)
    {
        current_health += newHealth;
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        max_health += newMaxHealth;
    }

    public void SetDamage(float newDamage)
    {
        Damage = newDamage;
    }

    public float GetDamage()
    {
        return Damage;
    }
    
    public void SetAttackTimer(float newTimer)
    {
        attack_timer = newTimer;
    }

    public float GetAttackTimer()
    {
        return attack_timer;
    }

    public void SetMoveSpeed(float newMoveSpeed)
    {
        move_speed = newMoveSpeed;
    }

    public float GetMoveSpeed()
    {
        return move_speed;
    }

    public float GetJumpImpulse()
    {
        return jump_impulse;
    }

    public void SetJumpImpulse(float newJumpImpulse)
    {
        jump_impulse = newJumpImpulse;
    }

    void Update() //Any updates that need to bypass physics go here
    {
        if (is_alive)
        {
            /// Fetch input and clamp it
            up_axis = ClampAxis(Input.GetAxis("Vertical"));
            side_axis = ClampAxis(Input.GetAxis("Horizontal"));

            Rotation();
            CheckFallen();
            Swing();
        }

        /// This can be added to another method as more UI functionality
        /// is added
        ui_hp_bar_inner.fillAmount = current_health * 0.01f;
    }
}
