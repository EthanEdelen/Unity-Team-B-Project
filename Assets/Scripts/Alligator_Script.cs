using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alligator_Script : Enemy_Script
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //Set in Prefab?
        //move_speed = 8.0f;
        //rotation = 0.0f;
        //rotate_speed = 100.0f;
        if (special)
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.material.color = new Color(0.6f, 0.0f, 0.7f);
            renderer = transform.GetChild(0).GetComponent<MeshRenderer>();
            renderer.material.color = new Color(0.6f, 0.0f, 0.7f);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        jump_time_elapsed += Time.deltaTime;
    }

    public override void FixedUpdate()
    {
        float temp = move_speed;
        GameObject player_object = GM_Script.GM.playerObject;
        if (player_object != null)
        {
            Player_Script player = player_object.GetComponent<Player_Script>();
            if (player != null)
            {
                // The step size is equal to speed times frame time.
                // Determine which direction to rotate towards
                float distance = Vector3.Distance(player.transform.position, transform.position);
                if (distance <= 10 && distance >= 7)
                {
                    Jumping();
                }
                
            }
        }
        is_grounded = CheckGrounded();
        Movement();
        move_speed = temp;
        base.FixedUpdate();
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        Player_Script player = collision.gameObject.GetComponent<Player_Script>();
        if (player != null)
            if (special)
            {
                player.Slow.Effect(collision.gameObject);
            }
    }
}
