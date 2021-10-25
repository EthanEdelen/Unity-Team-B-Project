using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Script : MonoBehaviour
{

    public GameObject owner; //The unit that fired this projectile.
    public int damage;
    public float lifeTime;
    public AudioSource audioSource;
    public AudioClip firedClip;
    public AudioClip hitClip;
    public MeshRenderer meshRenderer;
    public Rigidbody rb;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
        //Get our owner's character code.
        Character_Script character = owner.GetComponent<Character_Script>();
        damage += character.atk;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void SetVelocity(Vector3 fireVelocity)
    {
        rb.velocity = fireVelocity;
    }

    protected void OnTriggerEnter(Collider other) //Protected means that other projectiles can inherit this.
    {
        //Only trigger once when we enter.
        if(other.tag == "Player" || other.tag == "Enemy")
        {
            if (other.gameObject != owner) //If it's NOT hitting ourself!
            {

                other.GetComponent<Character_Script>().GetHit(damage);
            }
            else
            {
                Debug.Log("Ignoring self collision . . .");  //We hit ourself in confusion!
            }
        }
        else if(other.tag == "Environment")
        {

        }
        else if(other.tag == "Bounds")
        {
            Destroy(this.gameObject);  //Important!  "this" is the script, but gameObjects can exist without a script.
            //We must destroy the gameObject, because it also destroys the scrpit.
        }
        else
        {
            Debug.Log("There is no proper collider tag set for gameObject: \"" 
                + other.gameObject + "\" With tag: \"" + other.tag +"\"");
        }
    }
}
