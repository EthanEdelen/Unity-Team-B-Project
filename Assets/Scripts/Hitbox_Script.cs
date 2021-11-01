using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_Script : MonoBehaviour
{
    public Player_Script owner;
    public float timer = 5.0f;

    // used to find middle of spwaner box  - Is this supposed to be here?
    //private static Vector3 RandomPointInBox(Vector3 center, Vector3 size)
    //{

    //    return center + new Vector3(
    //       (Random.value - 0.5f) * size.x,
    //       (Random.value - 0.5f) * size.y,
    //       (Random.value - 0.5f) * size.z
    //    );
    //}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            /// Debug message
            print("Hit " + other.gameObject.name);

            /// Uncomment when take_damage() is implemented vvv !! GetGit(int damage) in char script
            other.gameObject.GetComponent<Character_Script>().GetHit(owner.GetDamage());
        }
    }

}
