using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectable_Manager : MonoBehaviour
{
    public float SpawnTimer = 5.0f;
    public PowerUP_Script PowerUp;
    public Debuff_Script Debuff;
    public CollectableSuper Super;
    public GameObject Collectable_Prefab;
    private Renderer renderer;
    
    // used to find a spawn point inside the SpawnerBox
    private static Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    {

        return center + new Vector3(
           (Random.value - 0.5f) * size.x,
           (Random.value - 0.5f) * size.y,
           (Random.value - 0.5f) * size.z
        );
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        PowerUp = GetComponent<PowerUP_Script>();
        Super = GetComponent<CollectableSuper>();
        Debuff = GetComponent<Debuff_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnTimer <= 0)
        {
            Spawning();
            SpawnTimer = 5;
        }
        else
            SpawnTimer -= Time.deltaTime;
    }

    //Randomly desiceds if the next drop is a powerup or debuff
    private int PickSpawnType()
    {
        int type = (int)Random.Range(0, 100);
        if ((type % 2) == 0)
            return 1;
        else
            return 0;
    }

    //sets the effects for the currently spawned object
    private void EffectSeeder(int Spawntype)
    {
        Debug.Log(Spawntype);
        if (Spawntype == 2)
        {
            int effect = (int)Random.Range(1, 6);
            Debug.Log(effect);
            switch(effect)
            {
                case 1:
                    PowerUp.ChoosePowerUP(PowerUP_Script.PowerUPType.HEALTH);
                    break;
                case 2:
                    PowerUp.ChoosePowerUP(PowerUP_Script.PowerUPType.ATTACKSPEED);
                    break;
                case 3:
                    PowerUp.ChoosePowerUP(PowerUP_Script.PowerUPType.DAMAGE);
                    break;
                case 4:
                    PowerUp.ChoosePowerUP(PowerUP_Script.PowerUPType.BONUSHEALTH);
                    break;
                case 5:
                    PowerUp.ChoosePowerUP(PowerUP_Script.PowerUPType.HOPS);
                    break;
                case 6:
                    PowerUp.ChoosePowerUP(PowerUP_Script.PowerUPType.RUSH);
                    break;         
            }
        }
        else
        {
            int effect = (int)(Random.Range(1, 5));
            Debug.Log(effect);
            switch (effect)
            {
                case 1:
                    Debuff.ChooseDebuff(Debuff_Script.DebuffType.FATIGUE);
                    break;
                case 2:
                    Debuff.ChooseDebuff(Debuff_Script.DebuffType.PLAGUE);
                    break;
                case 3:
                    Debuff.ChooseDebuff(Debuff_Script.DebuffType.WEAKNESS);
                    break;
                case 4:
                    Debuff.ChooseDebuff(Debuff_Script.DebuffType.IRONSHOES);
                    break;
                case 5:
                    Debuff.ChooseDebuff(Debuff_Script.DebuffType.SLOW);
                    break;
            }
        }
    }

    //handles putting the object in the world and making it do stuff
    private void Spawning()
    {
        //int type = PickSpawnType();
        //EffectSeeder(type);
        Vector3 SpawnPoint = RandomPointInBox(renderer.bounds.center, renderer.bounds.size);
        GameObject collectable = (GameObject)Instantiate(Collectable_Prefab, SpawnPoint, Quaternion.identity);
        
    }  
}
