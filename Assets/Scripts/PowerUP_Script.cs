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

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(myPowerUPType);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(myPowerUPType);
    }

    //chooses the powerup type, mostly for a seeder or manager
    public void ChoosePowerUP(PowerUPType type)
    {
        myPowerUPType = type;
    }

    //The effects that will happen to the player when picked up.
    public override void Effect(GameObject other)
    {
        Player_Script script = other.GetComponent<Player_Script>();
        switch (myPowerUPType)
        {
            case PowerUPType.HEALTH:
                {
                    if (script.GetCurrentHealth() == script.GetMaxHealth())
                        script.SetCurrentHealth(script.GetMaxHealth());
                    else
                        script.SetCurrentHealth(script.GetCurrentHealth() + 1);
                    break;
                }
            case PowerUPType.DAMAGE:
                {
                    script.SetDamage(script.GetDamage() * 2);
                    break;
                }
            case PowerUPType.ATTACKSPEED:
                {
                    script.SetAttackTimer(script.GetAttackTimer() / 2);
                    break;
                }
            case PowerUPType.BONUSHEALTH:
                {
                    script.SetMaxHealth(script.GetMaxHealth() + (script.GetMaxHealth() / 2));
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
}
