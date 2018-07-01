using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform startPos = null;

    //spawn time based
    public float delayMin = 1.5f;
    public float delayMax = 5;
    public float speedMin = 1;
    public float speedMax = 4;

    //spawn at start
    public bool useSpawnPlacement = false;
    public int spawnCountMin = 4;
    public int spawnCountMax = 20;

    private float lastTime = 0;
    private float delayTime = 0;
    private float speed = 0;

    public GameObject item = null;
    public bool goLeft = false;
    public float spawnleftpos = 0;
    public float spawnRightPos = 0;

    private List<float> positionUsed = new List<float>();

    private void Start()
    {
        if (useSpawnPlacement)
        {
            int spawnCount = Random.Range(spawnCountMin, spawnCountMax);

            for(int i = 0; i < spawnCount; i++)
            {
                SpawnItem();
            }
        }
        else
        {
            speed = Random.Range(speedMin, speedMax);
            SimulateStartItems();

            lastTime = Time.time;
            delayTime = Random.Range(delayMin, delayMax);
        }

    }
    
    void SimulateStartItems()
    {
        //int spawnCount = Random.Range(spawnCountMin, spawnCountMax);

        for(float simulatedTime = 15; simulatedTime > 0; simulatedTime -= Random.Range(delayMin, delayMax)) {
            GameObject obj = SpawnItem();

            float simulatedPosition = (goLeft) ? obj.transform.position.x - speed * simulatedTime : obj.transform.position.x + speed * simulatedTime;
            obj.transform.position = new Vector3(simulatedPosition, obj.transform.position.y, obj.transform.position.z);
        }       
    }

    private void Update()
    {
        if (useSpawnPlacement) return;

        if (Time.time > lastTime + delayTime)
        {
            lastTime = Time.time;

            delayTime = Random.Range(delayMin, delayMax);

            SpawnItem();
        }
    }

    private GameObject SpawnItem()
    {
        //GameObject obj = Instantiate(item) as GameObject;
        GameObject obj = ObjectPoolManager.Instance.GetObject(item.name);

        if(obj == null)
        {
            Debug.Log("we are trying to get an object of " + item.name + " but we got null instead");
        }

        obj.transform.position = GetSpawnPosition();

        float direction = 0;

        if (goLeft)
        {
            direction = 180;
        }

        if (!useSpawnPlacement)
        {
            obj.GetComponent<Mover>().speed = speed;

            obj.transform.rotation = obj.transform.rotation * Quaternion.Euler(0, direction, 0);
        }

        return obj;
    }


    

    Vector3 GetSpawnPosition()
    {
        if( useSpawnPlacement)
        {
            int x = (int)Random.Range(spawnleftpos, spawnRightPos);

            for (int i = 0; i < positionUsed.Count; i++)
            {
                if (positionUsed[i] == x)
                {
                    return GetSpawnPosition();
                }
            }

            positionUsed.Add(x);

            Vector3 pos = new Vector3(x , startPos.position.y, startPos.position.z);

            return pos;
        }
        else
        {
            return startPos.position;
        }
        
    }
}
