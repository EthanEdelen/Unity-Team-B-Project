using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_hitbox_script : MonoBehaviour
{

    public float damage = 5;
    public float knockback = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Object.Destroy(gameObject, 0.1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (collision.collider.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Character_Script>().GetHit(damage);
            Vector3 directionVector = (collision.gameObject.transform.position - gameObject.transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(directionVector * knockback);
        }
    }
}
