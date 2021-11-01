using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_Script : MonoBehaviour
{
    public float timer = 5.0f;

    // used to find middle of spwaner box
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
