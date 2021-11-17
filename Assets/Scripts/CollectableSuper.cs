using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSuper : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Used to apply the effects from higer level collectables classes
    private void ApplyEffeccts(GameObject target)
    {
        Effect(target);
    }

    //Checks if the collectable has been collected
    public virtual void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit");
            ApplyEffeccts(collision.gameObject);
            Destroy(this.gameObject);
            
           
        }

        //eneimes walking over pickups kill the pickup as of 10/23
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    private void EffectTimer(float Timer)
    {

    }

    //Pure virtual method to be overloaded by each diffrent type of pick up
    public virtual void Effect(GameObject other)
    {

    }

    public virtual void SetTimer(float NewTimer)
    {
  
    }

    public virtual void SetActive()
    {

    }
}