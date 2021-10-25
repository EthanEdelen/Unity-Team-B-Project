using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : Character_Script
{
    ///This is based off of Character script.  We must override it's virtual functions to expand them.
    ///Enemy_Script has all of the same variables, but we can add more.

    //Enemy Variables



    // Start is called before the first frame update
    public override void Start()  //The public override keywords allow us to borrow Character_Script's behaviors 
    {
        //We run everything in Character_script's Start() with base.Start();
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        //We run everything in Character_script's Update() with base.Update();
        base.Update();
    }

    //Note, we still run fixed update from character script without overriding it.

    public override void Death()
    {
        base.Death();
        GM_Script.GM.AddScore(score);
    }
}
