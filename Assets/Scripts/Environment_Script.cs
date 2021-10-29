using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_Script : MonoBehaviour
{
    public Vector3 WaveLocation;                       // Where the setpiece moves to for the wave to begin.
    public Vector3 DespawnSpot;                        // Where the setpiece moves to so it can despawn. (Usually InitialSpawn)

    public bool Unbounded = true;                      // This bool tracks if the setpiece spawns, moves, or is removed in an unusual manner.
                                                       // The Environment Manager does not use this setpiece to determine if everything is in place.

    public bool ReachedWaveLocation = false;           // This bool tracks when the setpiece has reached its position for the wave.
    public bool ReachedDespawnLocation = false;        // This bool tracks when the setpiece has reached its postion to despawn.

    public bool Education = false;                     // This bool tracks if the Environment Manager has given this setpiece its right information.
    public bool CurrentLayout = true;                  // This bool tracks if this setpiece is part of the current layout.

    public float MovementSpeed = 10.0f;               // Movement Speed used for bounding movement.
    public float MovementUsed = 0.0f;                  // How much Movement Speed is used - used to fake acceleration.

    private Vector3 MoveVector = Vector3.zero;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    public virtual void InfoHandoff(Vector3 waveLocation, Vector3 despawnSpot, bool unbounded)
    {
        WaveLocation = waveLocation;
        DespawnSpot = despawnSpot;
        Unbounded = unbounded;
        Education = true;
    }

    protected virtual void Movement()
    {
        Rigidbody my_rbody = GetComponent<Rigidbody>();
        if (!Unbounded & CurrentLayout & !ReachedWaveLocation)
        {
            float dist = Vector3.Distance(WaveLocation, this.transform.position);
            if (dist > 2.5f)
            {
                MovementUsed += 2.0f * Time.fixedDeltaTime;
                if (MovementUsed >= 1.0f)
                {
                    MovementUsed = 1.0f;
                }
                Vector3 diff = WaveLocation - this.transform.position;
                diff.Normalize();
                MoveVector = diff * MovementSpeed * MovementUsed * Time.fixedDeltaTime;
                my_rbody.MovePosition(this.transform.position + MoveVector);
            }
            else if (dist > 0.01f)
            {
                MovementUsed -= 2.0f * Time.fixedDeltaTime;
                if (MovementUsed <= 0.05f)
                {
                    MovementUsed = 0.05f;
                }
                Vector3 diff = WaveLocation - this.transform.position;
                diff.Normalize();
                MoveVector = diff * MovementSpeed * MovementUsed * Time.fixedDeltaTime;
                my_rbody.MovePosition(this.transform.position + MoveVector);
            }
            else
            {
                ReachedWaveLocation = true;
            }
        }
        else if (!Unbounded & ReachedWaveLocation & !CurrentLayout)
        {
            float dist = Vector3.Distance(DespawnSpot, this.transform.position);
            if (dist > 2.5f)
            {
                MovementUsed += 2.0f * Time.fixedDeltaTime;
                if (MovementUsed >= 1.0f)
                {
                    MovementUsed = 1.0f;
                }
                Vector3 diff = DespawnSpot - this.transform.position;
                diff.Normalize();
                MoveVector = diff * MovementSpeed * MovementUsed * Time.fixedDeltaTime;
                my_rbody.MovePosition(this.transform.position + MoveVector);
            }
            else if (dist > 0.01f)
            {
                MovementUsed -= 2.0f * Time.fixedDeltaTime;
                if (MovementUsed <= 0.05f)
                {
                    MovementUsed = 0.05f;
                }
                Vector3 diff = DespawnSpot - this.transform.position;
                diff.Normalize();
                MoveVector = diff * MovementSpeed * MovementUsed * Time.fixedDeltaTime;
                my_rbody.MovePosition(this.transform.position + MoveVector);
            }
            else
            {
                ReachedDespawnLocation = true;
            }
        }
        else
        {
            MovementUsed = 0.0f;
        }
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        if (Education)
        {
            Movement();
        }
    }
}
