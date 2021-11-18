using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Script : Enemy_Script
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (special)
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.material.color = new Color(1.0f, 0.0f, 1.0f);
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
        Movement();

        base.FixedUpdate();
        is_grounded = CheckGrounded();

        //Movement();
        //Rotation();
        //Fix_Rotation();
        Jumping();
        //CheckLife();
        //Commented code moved to enemy script fixed update.
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        Player_Script player = collision.gameObject.GetComponent<Player_Script>();
        if (player != null)
            if (special)
            {
                player.Ironshoes.Effect(collision.gameObject);
            }
    }
}
