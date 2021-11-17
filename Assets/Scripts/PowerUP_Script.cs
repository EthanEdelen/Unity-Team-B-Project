using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP_Script : CollectableSuper
{
    public enum PowerUPType
    {
        HEALTH,         //Gives the player health up-to their maximum health
        DAMAGE,         //Increases the damage that the players does to enimes
        ATTACKSPEED,    //Increases the attack speed of the player
        BONUSHEALTH,    //Adds health to the player that surpises thier max health
        RUSH,           //Increases the players move speed
        HOPS,           //Increases the players jump speed

    }
    public PowerUPType myPowerUPType;
    private float Timer;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(myPowerUPType);
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Taking out for temporary debugging
        //Debug.Log(myPowerUPType);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision); 
    }

    //chooses the powerup type, mostly for a seeder or manager
    public void ChoosePowerUP(PowerUPType type)
    {
        myPowerUPType = type;
    }

    public override void SetTimer(float NewTimer)
    {
        Timer = NewTimer;
    }

    //The effects that will happen to the player when picked up.
    public override void Effect(GameObject other)
    {
        Player_Script script = other.GetComponent<Player_Script>();
        switch (myPowerUPType)
        {
            case PowerUPType.HEALTH:
                {
                    //This will heal the player by an amount equal to their current health
                    //if (script.GetCurrentHealth() == script.GetMaxHealth())
                    //    script.SetCurrentHealth(script.GetMaxHealth());
                    //else
                    //    script.SetCurrentHealth(script.GetCurrentHealth() + 1);\

                    //This will porportionatley heal the character and truncate inside Heal()
                    script.Heal(script.GetMaxHealth() / 4);
                    break;
                }
            case PowerUPType.DAMAGE:
                {
                    script.SetAtk(script.GetAtk() * 2);
                    break;
                }
            case PowerUPType.ATTACKSPEED:
                {
                    script.SetFireRate(script.GetFireRate() / 2);
                    break;
                }
            case PowerUPType.BONUSHEALTH:
                {
                    script.AddMaxHealth(script.GetMaxHealth() + (script.GetMaxHealth() / 2));
                    break;
                }
            case PowerUPType.RUSH:
                {
                    script.SetMoveSpeed(script.GetMoveSpeed() + (script.GetMoveSpeed() / 2));
                    break;
                }
            case PowerUPType.HOPS:
                {
                    script.SetJumpImpulse(script.GetJumpImpulse() * 1.5f);
                    break;
                }

        }
    }

    public override void SetActive()
    {
        isActive = true;
    }

}
