using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script2 : Character_Script
{
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    ///This is Max's variation on the previously existing Player_Script.
    ///This is based off of Character script.  We must override it's virtual functions to expand them.
    ///Player_Script2 has all of the same variables, but we can add more.
    
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
}
