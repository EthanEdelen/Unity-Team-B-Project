using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff_Script : CollectableSuper
{
    public enum DebuffType
    {
        SLOW,       //Makes the Player move at a slower speed
        WEAKNESS,   //Makes the Players attacks do less damage
        PLAGUE,     //Makes the Players Max health lower
        FATIGUE,    //Makes the Player attack slower
        IRONSHOES,  //Lowers the PLayers jump height

    }
    public DebuffType myDebuff;
    private float Timer;
    private bool isActive;

    /// Set timer <=0 for permanent effect
    public float timer = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(myDebuff);
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }


    //chooses the Debuff type, mostly for a seeder or manager
    public void ChooseDebuff(DebuffType type)
    {
        myDebuff = type;
    }


    //The effects that will happen to the player for each type of Debuff
    public override void Effect(GameObject other)
    {
        Player_Script script = other.GetComponent<Player_Script>();
        if (script.debuff_timer_list[(int)myDebuff] <= 0)
        {

            switch (myDebuff)
            {
                case DebuffType.FATIGUE:
                    {
                        Debug.Log("Fatiqued");
                        script.SetMoveSpeed(script.GetMoveSpeed() / 2);
                        break;
                    }
                case DebuffType.WEAKNESS:
                    {
                        Debug.Log("Weak");
                        script.SetAtk(script.GetAtk() / 3);
                        break;
                    }
                case DebuffType.PLAGUE:
                    {
                        Debug.Log("Plagued");
                        if (script.GetMaxHealth() <= 2)
                        {
                            script.SetMaxHealth(2f);
                        }
                        else
                            script.AddMaxHealth(-2);
                        break;
                    }
                case DebuffType.SLOW:
                    {
                        Debug.Log("Slowed");
                        script.SetMoveSpeed(script.GetMoveSpeed() / 4);
                        break;
                    }
                case DebuffType.IRONSHOES:
                    {
                        Debug.Log("Ironshoes");
                        script.SetJumpImpulse(script.GetJumpImpulse() / 2);
                        break;
                    }
            }
        }
        script.debuff_timer_list[(int)myDebuff] = timer;
        Debug.Log(script.debuff_timer_list[(int)myDebuff]);
    }

    public void EndEffect(GameObject other)
    {
        Player_Script script = other.GetComponent<Player_Script>();
        switch (myDebuff)
        {
            case DebuffType.FATIGUE:
                {
                    Debug.Log("Fatiqued End");
                    script.SetMoveSpeed(script.GetMoveSpeed() * 2);
                    break;
                }
            case DebuffType.WEAKNESS:
                {
                    Debug.Log("Weak End");
                    script.SetAtk(script.GetAtk() * 3);
                    break;
                }
            case DebuffType.PLAGUE:
                {
                    Debug.Log("Plague End");
                    if (script.GetMaxHealth() <= 2)
                    {
                        script.SetMaxHealth(2f);
                    }
                    else
                        script.AddMaxHealth(2);
                    break;
                }
            case DebuffType.SLOW:
                {
                    Debug.Log("Slow End");
                    script.SetMoveSpeed(script.GetMoveSpeed() * 4);
                    break;
                }
            case DebuffType.IRONSHOES:
                {
                    Debug.Log("Ironshoes End");
                    script.SetJumpImpulse(script.GetJumpImpulse() * 2);
                    break;
                }
        }
    }

    public override void SetTimer(float NewTimer)
    {
        Timer = NewTimer;
    }

    private void ApplyEffect(DebuffType type, GameObject other)
    {
        Player_Script script = other.GetComponent<Player_Script>();
        switch (myDebuff)
        {
            case DebuffType.FATIGUE:
                {
                    script.SetAtk(script.GetAtk() / 2);
                    break;
                }
            case DebuffType.WEAKNESS:
                {
                    script.SetAtk(script.GetAtk() / 3);
                    break;
                }
            case DebuffType.PLAGUE:
                {
                    if (script.GetMaxHealth() <= 2)
                    {
                        script.SetMaxHealth(2f);
                    }
                    else
                        script.AddMaxHealth(script.GetMaxHealth() - 2);
                    break;
                }
            case DebuffType.SLOW:
                {
                    script.SetMoveSpeed(script.GetMoveSpeed() / 2);
                    break;
                }
            case DebuffType.IRONSHOES:
                {
                    script.SetJumpImpulse(script.GetJumpImpulse() / 2);
                    break;
                }
        }
    }

    public override void SetActive()
    {
        isActive = true;
    }

}
