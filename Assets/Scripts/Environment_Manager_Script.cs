using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public enum EnvironmentTypes // This may be expanded later
    {
        Basic // The basic, default platform. There should be an object class for each of these.
    }
    public struct EnvironmentSpawnInfo // This may be expanded later
    {
        public EnvironmentTypes Type;         // The type of platform, used to instantiate the object.
        public Vector3 InitalSpawn;    // Where the setpiece spawns at first, before it moves to its wave location. (This spawn is usually offscreen.)
        public Vector3 WaveLocation;   // The location the setpiece occupies during the wave.
        public Vector3 DespawnSpot;    // The location the setpiece moves to so it can despawn once the wave is over. (Usually the same as the inital spawn.) 
        public bool Unbounded;         // Sets if the setpiece spawns in an unusual way.
        public float ConstructDelay;   // The delay, in seconds, that the setpiece takes before it spawns.
        public float DeconstructDelay; // The delay, in seconds, that the setpiece takes before it begins despawning.
        
        public EnvironmentSpawnInfo(EnvironmentTypes type, Vector3 initalSpawn, Vector3 waveLocation, Vector3 despawnSpot, bool unbounded, float constructDelay, float deconstructDelay)
        {
            this.Type = type;
            this.InitalSpawn = initalSpawn;
            this.WaveLocation = waveLocation;
            this.DespawnSpot = despawnSpot;
            this.Unbounded = unbounded;
            this.ConstructDelay = constructDelay;
            this.DeconstructDelay = deconstructDelay;
        }
    }
    public struct LayoutInfo // This may be expanded later
    {
        public List<EnvironmentSpawnInfo> EnvironmentList; //A list of all the setpiece information needed for construction.
        public LayoutInfo(List<EnvironmentSpawnInfo> environmentList)
        {
            this.EnvironmentList = environmentList;
        }
    }

    public List<LayoutInfo> possibleLayouts;
    public List<EnvironmentSpawnInfo> environmentList;
    public LayoutInfo currentLayout;

    public int waveSelector = 0;
    public int lastWaveSelector = -1;

    public List<GameObject> setpieces;

    public bool firstTime = true;

    public GameObject basicPlatform;

    GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        setpieces = new List<GameObject>();
        possibleLayouts = new List<LayoutInfo>();
        environmentList = new List<EnvironmentSpawnInfo>();
        environmentList.Add(new EnvironmentSpawnInfo(EnvironmentTypes.Basic, new Vector3(-13, -2, 22), new Vector3(-13, 6, 22), new Vector3(-13, -2, 22), false, 0.0f, 0.0f)); 
        possibleLayouts.Add(new LayoutInfo(environmentList));
        environmentList = new List<EnvironmentSpawnInfo>();
        environmentList.Add(new EnvironmentSpawnInfo(EnvironmentTypes.Basic, new Vector3(-13, -2, -17), new Vector3(-13, 6, -17), new Vector3(-13, -2, -17), false, 0.0f, 0.0f));
        possibleLayouts.Add(new LayoutInfo(environmentList));
        spawner = GameObject.FindWithTag("Respawn");
    }

    void Idling()
    {
        //stateTimer -= Time.deltaTime;
        //if (stateTimer < 0.0f)
        //{
            //stateTimer = 5.0f;
            currentState = ManagerState.Layout_Selection;
        //}
    }

    void Layout_Selection()
    {
        
        waveSelector = Random.Range(0, 2);
        Debug.Log("Layout selection . . .");
        currentLayout = possibleLayouts[waveSelector];
        currentState = ManagerState.Augmentation;
        return;
    }

    void Augmentation()
    {
        currentState = ManagerState.Layout_Construction;
        return;
    }

    void Layout_Construction()
    {
        Debug.Log("Layout Constructor . . .");
        if (waveSelector == lastWaveSelector)
        {
            currentState = ManagerState.Enemy_Spawntime;
            return;
        }
        if (firstTime)
        {
            int i = 0;
            if (lastWaveSelector >= 0)
            {
                for (; i < setpieces.Count; i++)
                {
                    setpieces[i].GetComponent<Environment_Script>().CurrentLayout = false;
                }
            }
            for (int j = 0; j < currentLayout.EnvironmentList.Count;j++)
            {
                if (currentLayout.EnvironmentList[j].Type == EnvironmentTypes.Basic)
                {
                    setpieces.Add(Instantiate(basicPlatform, currentLayout.EnvironmentList[j].InitalSpawn, transform.rotation * Quaternion.AngleAxis(-90, Vector3.right)));
                    setpieces[i].GetComponent<Environment_Script>().InfoHandoff(currentLayout.EnvironmentList[j].WaveLocation, currentLayout.EnvironmentList[j].DespawnSpot, currentLayout.EnvironmentList[j].Unbounded);
                    i++;
                }
            }
        }
        bool notFinished = false;
        for (int k = 0; k < setpieces.Count; k++)
        {
            if (!setpieces[k].GetComponent<Environment_Script>().Unbounded & !setpieces[k].GetComponent<Environment_Script>().ReachedWaveLocation & setpieces[k].GetComponent<Environment_Script>().CurrentLayout & setpieces[k].GetComponent<Environment_Script>().Education)
            {
                notFinished = true;
            }
            else if (!setpieces[k].GetComponent<Environment_Script>().Unbounded & !setpieces[k].GetComponent<Environment_Script>().ReachedDespawnLocation & !setpieces[k].GetComponent<Environment_Script>().CurrentLayout & setpieces[k].GetComponent<Environment_Script>().Education)
            {
                notFinished = true;
            }
            else if (!setpieces[k].GetComponent<Environment_Script>().Unbounded & setpieces[k].GetComponent<Environment_Script>().ReachedDespawnLocation & !setpieces[k].GetComponent<Environment_Script>().CurrentLayout & setpieces[k].GetComponent<Environment_Script>().Education)
            {
                Destroy(setpieces[k]);
                setpieces.RemoveAt(k);
                k--;
            }
        }
        if (notFinished == false)
        {
            currentState = ManagerState.Enemy_Spawntime;
            return;
        }
        firstTime = false;
    }

    void Enemy_Spawntime()
    {
        Debug.Log("Enemy spawntime . . .");
        lastWaveSelector = waveSelector;
        firstTime = true;
        //stateTimer -= Time.deltaTime;
        //if (stateTimer < 0.0f) //Change this instead to where all of the enemies are spawned
        //{
        //    stateTimer = 5.0f;
        //    currentState = ManagerState.Wave_In_Progress;
        //}

        //print(spawner.GetComponent<Wave_Script>().enemies_spawned);
        if (spawner!= null)
        {
            if(spawner.GetComponent<Wave_Script>().enemies_spawned)
                currentState = ManagerState.Wave_In_Progress;
        }
    }

    void Wave_In_Progress()
    {
        //Debug.Log("Wave in progress . . .");
        //stateTimer -= Time.deltaTime;
        //if (stateTimer < 0.0f) // Change this instead to where all of the enemies are killed
        //{
        //    stateTimer = 5.0f;
        //    currentState = ManagerState.Wave_Complete;
        //}

        //print(spawner.GetComponent<Wave_Script>().wave_completed);

        if (spawner.GetComponent<Wave_Script>().wave_completed)
        {
            currentState = ManagerState.Wave_Complete;
        }
    }

    void Wave_Complete()
    {
        Debug.Log("Wave complete . . .");
        stateTimer -= Time.deltaTime;
        print(stateTimer);
        if (stateTimer < 0.0f)
        {
            spawner.GetComponent<Wave_Script>().SetReady(true);
            stateTimer = 5.0f;
            currentState = ManagerState.Layout_Selection;
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
                Layout_Selection();
                break;
            case ManagerState.Augmentation:
                Augmentation();
                break;
            case ManagerState.Layout_Construction:
                Layout_Construction();
                break;
            case ManagerState.Enemy_Spawntime:
                Enemy_Spawntime();
                break;
            case ManagerState.Wave_In_Progress:
                Wave_In_Progress();
                break;
            case ManagerState.Wave_Complete:
                Wave_Complete();
                break;
            default:
                currentState = ManagerState.Idling;
                break;
        }
    }
}
