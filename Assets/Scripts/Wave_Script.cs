using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave_Script : MonoBehaviour
{
    public int wave = 1;
    public int total_difficulty = 1;
    int difficulty_remaining = 1;
    public Transform SpawnPosition;
    public GameObject enemy_object;
    List<GameObject> enemy_list = new List<GameObject>();

    bool wave_completed = true;
    public GameObject slime_prefab;
    public GameObject fly_prefab;
    public GameObject alligator_prefab;
    // Start is called before the first frame update
    void Start()
    {
        //Slime_Script slime_script = slime_prefab.GetComponent<Slime_Script>();
        create_list();
    }

    // Update is called once per frame
    void Update()
    {
        //Bullet bullet = bullet_object.GetComponent<Bullet>();
        if(wave_completed)
        {
            // pick an enemy whose score can fit the wave and is 

            //while (difficulty_remaining > 0)
            //{
            //    //int list_size = enemy_list.Count;
            //    //int list_value = Random.Range(0, list_size - 1);
            //    //enemy_object = Instantiate(enemy_list[list_value], transform.position, Quaternion.identity);
            //    //int enemy_difficulty = enemy_object.GetComponent<Enemy_Script>();
            //    //difficulty_remaining -= enemy_difficulty;
            //}
            //enemy_list.Clear();
            //create_list();
            wave_completed = false;
        }
    }

    void create_list()
    {
        enemy_list.Add(alligator_prefab);
        enemy_list.Add(fly_prefab);
        enemy_list.Add(slime_prefab);
    }
}
