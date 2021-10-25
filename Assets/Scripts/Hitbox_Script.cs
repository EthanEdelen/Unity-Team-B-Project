using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            /// Debug message
            print("Hit " + other.gameObject.name);

            /// Uncomment when take_damage() is implemented vvv
            //other.gameObject.GetComponent<EnemyScript>().take_damage();
        }
    }
}
