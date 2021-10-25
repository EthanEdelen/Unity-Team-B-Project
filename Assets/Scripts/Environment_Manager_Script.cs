using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Environment_Manager_Script : MonoBehaviour
{
    enum ManagerState
    {
        Idling,              //Waits for a few seconds. Used when the game begins, mostly.
        Layout_Selection,    //Selects the next (randomized) arena layout according to current difficulty, and preps it to be used in the game.
        Augmentation,        //If the difficulty has increased enough, this state will modify existing layouts to make them more difficult.
        Layout_Construction, //The Manager waits for all of the new layout's environment to be moved into place before continuing.
        Enemy_Spawntime,     //The Manager only switches out of this state once all enemies for the wave have been spawned.
        Wave_In_Progress,    //The Manager waits for the player to kill all the enemies, or until a timer runs out. May spawn additional hazards.
        Wave_Complete,       //Lasts long enough for the player to catch their breath. Goodies may be spawned. Exits to Layout_Selection.

    }
    ManagerState currentState = ManagerState.Idling;
    float stateTimer = 5.0f;

    enum EnvironmentTypes
    {
        Static //The basic, default platform. There should be an object class for each of these.
    }
    struct EnvironmentSpawnInfo
    {
        public Vector3 InitalSpawn;  //Where the setpiece spawns at first, before it moves to its wave location. (This spawn is usually offscreen.)
        public Vector3 WaveLocation; //The location the setpiece occupies during the wave.
        public Vector3 DespawnSpot;  //The location the setpiece moves to so it can despawn once the wave is over. (Usually the same as the inital spawn.)
        float SpawnTime;             //When during Layout_Construction the setpiece spawns, given its layout is being constructed.
        float DespawnTime;           //When during Layout_Construction the setpiece begins the process of despawning, if it is part of an old wave.
        public EnvironmentSpawnInfo(Vector3 initalSpawn, Vector3 waveLocation, Vector3 despawnSpot, float spawnTime, float despawnTime)
        {
            this.InitalSpawn = initalSpawn;
            this.WaveLocation = waveLocation;
            this.DespawnSpot = despawnSpot;
            this.SpawnTime = spawnTime;
            this.DespawnTime = despawnTime;
        }
    }
    struct LayoutInfo
    {
        public EnvironmentSpawnInfo[] EnvironmentList; //A list of all the setpiece information needed for construction.
        float LayoutConstructionTime;                  //How long it takes for this layout to construct itself.
        float LayoutDestructionTime;                   //How long it takes for this layout to deconstruct itself.
        public LayoutInfo(EnvironmentSpawnInfo[] environmentList, float layoutConstructionTime, float layoutDestructionTime)
        {
            this.EnvironmentList = environmentList;
            this.LayoutConstructionTime = layoutConstructionTime;
            this.LayoutDestructionTime = layoutDestructionTime;
        }
    }

    LayoutInfo[] possibleLayouts;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Add all the layouts as appends here.
    }

    void Idling()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer < 0.0f)
        {
            stateTimer = 5.0f;
            currentState = ManagerState.Idling;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case ManagerState.Idling:
                Idling();
                break;
            case ManagerState.Layout_Selection:
                Idling();
                break;
            default:
                currentState = ManagerState.Idling;
                break;
        }
    }
}
