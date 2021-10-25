using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Character_Script : MonoBehaviour
{
    //Note, I tend to code in camel casing, hope that's allright.
    //Game Stats Variables
    public int hp;
    public int maxHp;
    public int atk;  //Used in damage calculations this character is responsible for.
    public float fireRate;
    public int score; //How many points this character is worth or has accumalated.
    public bool alv;

    //Timer Variables
    public float lastFired;
    public float immunityFrames;  //How many seconds of invincibility the character CURRENTLY has.
    public float immunityGain;  //How many seconds of invincibility the character gains when hit.

    //Game object variables
    public MeshRenderer meshRenderer;
    public Animator animator;
    //public Collider collider;  //Incorrect, there's already a gameObject.collider in monoBehavior

    //
    public AudioSource audioSource;
    public AudioClip hurtClip;
    public AudioClip deathClip;
    public AudioClip spawnClip;

    //Game UI
    public TextMeshProUGUI healthText;
    public Slider hpSlider;

    //Game Input Variables
    public Vector2 moveAxis;
    private bool lockMovement;
    private bool lockInput;
    //While the character isBusy doing certain actions such as attacking, they cannot perform others such as firing.
    private bool isBusy;  


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
        Movement();
    }
    //Is called 60 times a second regardless of frame rate.
    public virtual void FixedUpdate()
    {
        float dt = Time.deltaTime;
        UpdateTimers(dt);
    }

    public virtual void UpdateUI()
    {
        hpSlider.value = (float)hp / (float)maxHp; //Returns a 0 - 1 value to map to the slider.
    }
    public virtual void UpdateTimers(float dt)
    {
        if (lastFired < fireRate)
            lastFired += dt;
        if (immunityFrames > 0)
            immunityFrames -= dt;
    }
    //This function isn't the same as a set hp, we need to set I frames and play audio, plausibly a visual indicator too.
    public virtual void GetHit(int damage)
    {
        //Talk shit, get hit.
        if (immunityFrames > 0)
        {
            hp -= damage;
            audioSource.PlayOneShot(hurtClip);
            if (hp <= 0)
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
        Destroy(gameObject, 2);  //Kill me in two seconds, enough for a dramatic death.
    }
    public virtual void Movement()
    {

    }
}
