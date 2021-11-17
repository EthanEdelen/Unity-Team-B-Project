using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Movement : MonoBehaviour
{

    //int move_direction;
    //float move_speed;
    //float countdown;
    int xspacing;
    int width;
    float theta;
    float amp;
    float period;
    float dx;
    float[] yvalues;

    
    void Start()
    {
        //move_direction = 1;
        //move_speed = 1.5f;
        //countdown = 0.6f;
    }

    void FixedUpdate()
    {
        //transform.Translate(new Vector3(0, 0, move_speed * move_direction * Time.fixedDeltaTime));
        //if (transform.position.y >= .5 || transform.position.y <= -.5)
        //{
            //move_direction *= -1;
        //}
        /*countdown -= Time.fixedDeltaTime;
        if(countdown <= 0)
        {
            move_direction *= -1;
            countdown = 1;
        }*/
    }
}
