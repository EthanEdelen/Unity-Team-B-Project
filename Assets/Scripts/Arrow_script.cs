using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Object.Destroy(gameObject, 1.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check to see if the tag on the collider is equal to Enemy
        if (collision.collider.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
