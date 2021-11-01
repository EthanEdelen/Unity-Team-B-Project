using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    private float attack_timer = .5f;
    public float Damage = 10.0f;
    public float max_health = 5.0f;
    public float current_health = 3.0f;
    public float move_speed = 8000.0f;
    public float jump_impulse = 1000000.0f;
    public float rotation = 0.0f;
    private float up_axis = 0.0f;
    private float side_axis = 0.0f;
    private bool is_grounded = true;
    private bool is_swinging = false;
    private bool slash_held = false;
    private float swing_rotation = -90.0f;
    private GameObject swordHitbox;
    // Start is called before the first frame update
    void Start()
    {
        swordHitbox = GameObject.Find("Sword Hitbox");
        swordHitbox.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate() // This should be used for physics updates
    {
        is_grounded = CheckGrounded();
        AdjustFriction();
        Jumping();
        Movement();
        Swing();
    }

    void Movement()
    {
        Rigidbody my_rbody = GetComponent<Rigidbody>();
        this.transform.eulerAngles = new Vector3(0, rotation, 0);
        if (up_axis != 0.0 | side_axis != 0.0)
        {
            Vector3 move_vec = Vector3.right * -move_speed * Time.fixedDeltaTime;
            my_rbody.AddRelativeForce(move_vec, ForceMode.Impulse);
        }
    }

    void Rotation()
    {
        up_axis = Input.GetAxis("Vertical");
        side_axis = Input.GetAxis("Horizontal");
        if (up_axis == 0.0 & side_axis == 0.0)
        {
        }
        else if (up_axis > 0.0 & side_axis == 0.0)
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
        }
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

    void Swing()
    {
        float swing_axis = Input.GetAxis("Slash");
        if (is_swinging == false & swing_axis != 0.0f & slash_held == false)
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
        Rotation();
    }
}
