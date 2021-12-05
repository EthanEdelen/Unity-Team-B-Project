using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_script : MonoBehaviour
{
    public AudioClip Explosion;

    public GameObject bomb_hitbox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Object.Destroy(gameObject, 1.75f);
    }

    void OnDestroy()
    {
        GameObject Hitbox = Instantiate(bomb_hitbox, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(Explosion, transform.position);
    }
}
