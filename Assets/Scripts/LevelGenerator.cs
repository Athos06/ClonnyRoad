using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public List<GameObject> platform = new List<GameObject>();
    public List<float> height = new List<float>();

    private int rndRange = 0;
    private float lastPos = 0;
    private float lastScale = 0;

    private int lastGeneratedTile = -1;
    private int repetitionCount = 0;

    public List<GameObject> startGroundList = new List<GameObject>();
    private Queue<GameObject> groundList = new Queue<GameObject>();


    /// <summary>
    /// When we have a doble GroundTile it will removed only when we advanced twice, we set this flag to true to indicate that we dont destroy it the first time but the second time we advance
    /// </summary>
    private bool markToDestroyNext = false;

    private void Awake()
    {
        foreach(GameObject go in startGroundList)
        {
            groundList.Enqueue(go);
        }
    }

    public void RandomGenerator()
    {
        rndRange = Random.Range(0, platform.Count);


        if ( (lastGeneratedTile == 0 || lastGeneratedTile == 1 ) && (rndRange == 0 || rndRange == 1) )
        {
            repetitionCount++;
            if ( repetitionCount >= 2)
            {
                RandomGenerator();
                return;
            }
        }

        lastGeneratedTile = rndRange;

        for(int i = 0; i < platform.Count; i++)
        {
            CreateLevelObject(platform[i], height[i], i);
        }
    }

    public void CreateLevelObject( GameObject obj, float height, int value)
    {
        if( rndRange == value)
        {
            GameObject go = Instantiate(obj) as GameObject;

            groundList.Enqueue(go);

            float offset = lastPos + (lastScale * 0.5f);
            offset += (go.transform.localScale.z) * 0.5f;
            Vector2 pos = new Vector3(0, height, offset);

            go.transform.position = new Vector3(0, height, offset);

            lastPos = go.transform.position.z;
            lastScale = go.transform.localScale.z;

            go.transform.parent = this.transform;

        }
        
    }

    public void RemovedOldGround()
    {
        if(groundList.Peek().transform.localScale.z == 2 )
        {
            if (markToDestroyNext)
            {
                Destroy(groundList.Dequeue());
                markToDestroyNext = false;
            }
            else
            { 
                markToDestroyNext = true;
            }
        }
        else
        {
            Destroy(groundList.Dequeue());
        }
        

        //TODO move the deathBox forward
    }
}
