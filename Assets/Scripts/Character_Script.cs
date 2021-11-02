using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Character_Script : MonoBehaviour
{
    //Note, I tend to code in camel casing, hope that's allright.
    //Game Stats Variables
    public float current_health;
    public float max_health;
    public float atk;  //Used in damage calculations this character is responsible for.
    public float fireRate;
    public int score; //How many points this character is worth or has accumalated.
    public bool alv;

    //Platforming Variables
    public float move_speed = 10.0f;
    public float jump_impulse = 10000.0f;
    public float rotation = 0.0f;
    public float rotate_speed = 100.0f;
    public bool is_grounded = true;
    

    //Timer Variables
    public float lastFired;
    public float immunityFrames;  //How many seconds of invincibility the character CURRENTLY has.
    public float immunityGain;  //How many seconds of invincibility the character gains when hit.

    //Component and Object variables
    public MeshRenderer meshRenderer;
    public Animator animator;
    public GameObject bulletObject;
    public GameObject attackObject;  //Stores a gameObject for this character's Attack/Projectile, if any.
    public Transform firePort;  //The position to spawn ANY attacks!

    //public Collider collider;  //Incorrect, there's already a gameObject.collider in monoBehavior

    //
    public AudioSource audioSource;
    public AudioClip hurtClip;
    public AudioClip deathClip;
    public AudioClip spawnClip;
    public AudioClip jumpClip;
    public AudioClip attackClip;

    //Game UI
    public TextMeshProUGUI healthText;
    public Slider hpSlider;

    //Game Input Variables
    public Vector2 moveAxis;
    protected bool lockMovement;  //While true, player cannot Input movement
    protected bool lockInput; //While true, player can't input anything!
    
    //While the character "isBusy" doing certain actions such as attacking, they cannot perform other actions such as firing.
    //But, could still move and such.
    protected bool isBusy;  

    /// <summary>
    /// UPDATING FUNCTIONS
    /// </summary>
    // Start is called before the first frame update
    public virtual void Start()
    {
        audioSource.PlayOneShot(spawnClip);
    }

    // Update is called once per frame.
    //This means it should be used for input, fire rates, visuals etc.
    public virtual void Update()
    {
        UpdateUI();
        //Movement();
    }
    //Is called 60 times a second regardless of frame rate.
    public virtual void FixedUpdate()
    {
        float dt = Time.deltaTime;
        UpdateTimers(dt);
    }


    /// <summary>
    /// PLATFORMING  AND INPUT FUNCTIONS
    /// </summary>
    /// 
    
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


    /// <summary>
    /// STAT FUNCTIONS
    /// </summary>
    public virtual void UpdateUI()
    {
        hpSlider.value = current_health / max_health; //Returns a 0 - 1 value to map to the slider.
    }
    public virtual void UpdateTimers(float dt)
    {
        if (lastFired < fireRate)
            lastFired += dt;
        if (immunityFrames > 0)
            immunityFrames -= dt;
    }
    //This function isn't the same as a set hp, we need to set I frames and play audio, plausibly a visual indicator too.
    public virtual void GetHit(float damage)
    {
        //Talk shit, get hit.
        if (immunityFrames <= 0)
        {
            current_health -= damage;
            immunityFrames += immunityGain;
            audioSource.PlayOneShot(hurtClip);
            if (current_health <= 0)
            {
                Death();
            }
        }
        else
        {
            Debug.Log(gameObject + " is immune");
        }
     

    }
    public virtual void Death()
    {
        audioSource.PlayOneShot(deathClip);
        Debug.Log(gameObject + " has Died . . .");
        lockInput = true;
        lockMovement = true;
        Destroy(gameObject, 1);  //Kill me in one second, enough for a dramatic death.
    }
    public virtual void Movement()
    {
        Debug.Log("Target isn't moving . . .");
    }

    public virtual void Fire()
    {
        Debug.Log(gameObject + " Fired an attack!");
        GameObject bO = Instantiate(bulletObject);
        bO.transform.SetPositionAndRotation(firePort.position, firePort.rotation);
    }

    ///STAT GETTERS AND SETTER
    public float GetCurrentHealth()
    {
        return current_health;
    }

    public float GetMaxHealth()
    {
        return max_health;
    }

    public void Heal(float healValue)
    {
        current_health += healValue;
        //Truncate health if over max.
        if(current_health >= max_health)
        {
            current_health = max_health;
        }
    }
    //Optional function?
    public void AddMaxHealth(float newMaxHealth)
    {
        max_health += newMaxHealth;
    }
    public void SetMaxHealth(float newMaxHealth)
    {
        max_health = newMaxHealth;
    }

    public void SetAtk(float newDamage)
    {
        atk = newDamage;
    }

    public float GetAtk()
    {
        return atk;
    }

    //public void SetAttackTimer(float newTimer) //Replaced by fireRate
    //{
    //    attack_timer = newTimer;
    //}

    //public float GetAttackTimer()
    //{
    //    return attack_timer;
    //}
    public float GetFireRate() //Replaces attack timer
    {
        return fireRate;
    }

    public void SetFireRate(float newFireRate)
    {
        fireRate = newFireRate;
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
    
}
