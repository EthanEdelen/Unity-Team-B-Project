using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_Script : MonoBehaviour
{
    public Transform SpawnPosition;
    public GameObject enemy_object;
    List<GameObject> enemy_list = new List<GameObject>();
    public int current_wave = 0;

    public bool wave_completed = true;
    public bool enemies_spawned = false;
    public GameObject slime_prefab;
    public GameObject fly_prefab;
    public GameObject alligator_prefab;
    public GameObject turtle_prefab;

    public bool ready = true;
    public float spawn_time = 5;
    public float spawn_time_elapsed;

    public int max_enemy_spawns = 8;
    public int max_enemy_difficulty = 5;
    private List<GameObject> enemy_types_spawnlist = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Add all possible enemy types to spawn in waves - Difficulty level affects spawn chance.
        int slime_dif_level = slime_prefab.GetComponent<Enemy_Script>().difficulty_value;
        int alligator_dif_level = alligator_prefab.GetComponent<Enemy_Script>().difficulty_value;
        int fly_dif_level = fly_prefab.GetComponent<Enemy_Script>().difficulty_value;
        int turtle_dif_level = fly_prefab.GetComponent<Enemy_Script>().difficulty_value;

        // Slime
        for (int i = (max_enemy_difficulty + 1) - slime_dif_level; i > 0; i--)
        {
            enemy_types_spawnlist.Add(slime_prefab);
        }
        // Alligator
        for (int i = (max_enemy_difficulty + 1) - alligator_dif_level; i > 0; i--)
        {
            enemy_types_spawnlist.Add(alligator_prefab);
        }
        // Fly
        for (int i = (max_enemy_difficulty + 1) - fly_dif_level; i > 0; i--)
        {
            enemy_types_spawnlist.Add(fly_prefab);
        }
        // Turtle
        for (int i = (max_enemy_difficulty + 1) - turtle_dif_level; i > 0; i--)
        {
            enemy_types_spawnlist.Add(turtle_prefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Bullet bullet = bullet_object.GetComponent<Bullet>();
        if (wave_completed && !ready)
        {
            spawn_time_elapsed += Time.deltaTime;
            if (spawn_time_elapsed >= spawn_time)
                ready = true;
        }

        if (wave_completed && ready)
        {
            current_wave++;
            //Create_List(current_wave);
            Create_List_VarWave(current_wave);
            for (int i = 0; i < enemy_list.Count; i++)
            {
                enemy_object = Instantiate(enemy_list[i], new Vector3(SpawnPosition.position.x + Random.Range(-3, 3), SpawnPosition.position.y + Random.Range(0, 10), SpawnPosition.position.z + Random.Range(-12, 12)), Quaternion.identity);
            }
            enemy_list.Clear();
            wave_completed = false;
            enemies_spawned = true;
            ready = false;
        }

        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Enemy");

        if (gameObjects.Length == 0)
        {
            wave_completed = true;
            enemies_spawned = false;
        }
    }

    public void SetReady(bool isReady)
    {
        ready = isReady;
    }
    //Changed naming convention to capital

    /* Old hardcoded wave method - Deprecated
    void Create_List(int wave)
    {
        switch(wave)
        {
            case 1:
                enemy_list.Add(alligator_prefab);
                //enemy_list.Add(fly_prefab);
                enemy_list.Add(slime_prefab);
                break;
            case 2:
                enemy_list.Add(alligator_prefab);
                enemy_list.Add(alligator_prefab);
                enemy_list.Add(alligator_prefab);
                //enemy_list.Add(fly_prefab);
                enemy_list.Add(slime_prefab);
                break;
            case 3:
                enemy_list.Add(alligator_prefab);
                enemy_list.Add(alligator_prefab);
                enemy_list.Add(alligator_prefab);
                //enemy_list.Add(fly_prefab);
                enemy_list.Add(slime_prefab);
                enemy_list.Add(slime_prefab);
                enemy_list.Add(slime_prefab);
                enemy_list.Add(alligator_prefab);
                enemy_list.Add(alligator_prefab);
                enemy_list.Add(alligator_prefab);
                enemy_list.Add(slime_prefab);
                enemy_list.Add(slime_prefab);
                enemy_list.Add(slime_prefab);
                enemy_list.Add(slime_prefab);
                break;
            default:
                enemy_list.Add(alligator_prefab);

                print("invalid wave #");
                break;
        }
    }
    */
    void Create_List_VarWave(int wave)
    {
        int enemySpawnCounter = wave;
        if (enemySpawnCounter > max_enemy_spawns)
            enemySpawnCounter = max_enemy_spawns;
        for (int i = enemySpawnCounter; i > 0; i--)
        {
            int random_enemy = Random.Range(0, enemy_types_spawnlist.Count);
            enemy_list.Add(enemy_types_spawnlist[random_enemy]);
        }
    }
}