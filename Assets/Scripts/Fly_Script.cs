using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_Script : Enemy_Script
{
    bool focus = false;
    float focus_span = .2f;
    float focus_elapsed = 0;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        focus_elapsed += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        FlyMovement();
        base.FixedUpdate();
    }

    public void FlyMovement()
    {
        Rigidbody my_rbody;
        my_rbody = GetComponent<Rigidbody>();

        if(focus_elapsed >= focus_span)
        {
            focus_elapsed = 0;
            focus = !focus;
            if (!focus)
            {
                Vector3 move_vec = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * move_speed * Time.fixedDeltaTime;
                my_rbody.velocity = move_vec;
            }
        }    

        if (focus)
        {
            Vector3 move_vec = transform.forward * move_speed * Time.fixedDeltaTime;
            my_rbody.velocity = move_vec;
        }

    }
}
