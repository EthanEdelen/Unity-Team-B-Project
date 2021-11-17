using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(0, -5 * Time.fixedDeltaTime, 0));
        //Debug.Log(transform.position);
        if(transform.position.z > 60)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 116);
        }
    }
}
