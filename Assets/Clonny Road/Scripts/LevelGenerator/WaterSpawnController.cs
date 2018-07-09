using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawnController : MonoBehaviour
{

    public bool goLeft = false;
    public bool goRight = false;

    public List<GameObject> items = new List<GameObject>();
    public List<Spawner> spawnersRight = new List<Spawner>();
    public List<Spawner> spawnersLeft = new List<Spawner>();

    private static bool waterDirection;
    private static bool firstTime = true;

    private void Start()
    {
        int itemId = Random.Range(0, items.Count);
        GameObject item = items[itemId];

        if (firstTime) { 
            //true to the right, false to the left
            waterDirection = Random.Range(0, 2) > 0 ?  true : false;
            firstTime = false;
        }

        if (waterDirection)
        {
            goLeft = false;
            goRight = true;
            //waterDirection = false;
        }
        else
        {
            goLeft = true;
            goRight = false;
            //waterDirection = true;
        }

        for (int i = 0; i < spawnersLeft.Count; i++)
        {
            spawnersLeft[i].item = item;
            spawnersLeft[i].goLeft = goLeft;
            spawnersLeft[i].gameObject.SetActive(goRight);
            spawnersLeft[i].spawnleftpos = spawnersLeft[i].transform.position.x;

            goLeft = !goLeft;
            goRight = !goRight;
        }

        for (int i = 0; i < spawnersRight.Count; i++)
        {
            spawnersRight[i].item = item;
            spawnersRight[i].goLeft = goLeft;
            spawnersRight[i].gameObject.SetActive(goLeft);
            spawnersRight[i].spawnleftpos = spawnersRight[i].transform.position.x;

            goLeft = !goLeft;
            goRight = !goRight;
        }

    }

}
