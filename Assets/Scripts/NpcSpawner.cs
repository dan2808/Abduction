using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    public GameObject npc;
    public float secondsBetweenSpawns = 7;
    float nextSpawnTime;

    //maximum number of npcs
    public int maxNPC = 10;
    int currentNPCindex;

    //bottom middle location of the screen taking in account npc's middle height and dividing it in half( by 2) 
    private Vector2 bottomMiddleScreenLocation;
    void Start()
    {
        bottomMiddleScreenLocation.x = Camera.main.aspect * Camera.main.orthographicSize;
        bottomMiddleScreenLocation.y = Camera.main.orthographicSize - npc.transform.localScale.y / 2f;


        // Vector3 spawnPosition = new Vector3(Random.Range(-bottomMiddleScreenLocation.x, bottomMiddleScreenLocation.x), -bottomMiddleScreenLocation.y, 0);
        // Instantiate(npc, spawnPosition, Quaternion.identity);

    }

    void Update()
    {
        //StartCoroutine(Spawn());

        if (Time.time > nextSpawnTime && currentNPCindex < maxNPC)
        {
            nextSpawnTime = Time.time + secondsBetweenSpawns;

            //random spawnPosition
            Vector3 spawnPosition = new Vector3(Random.Range(-bottomMiddleScreenLocation.x, bottomMiddleScreenLocation.x), -bottomMiddleScreenLocation.y, 0);
            Instantiate(npc, spawnPosition, Quaternion.identity);
            currentNPCindex += 1;

        }
    }

    IEnumerator Spawn()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-bottomMiddleScreenLocation.x, bottomMiddleScreenLocation.x), -bottomMiddleScreenLocation.y, 0);
        Instantiate(npc, spawnPosition, Quaternion.identity);
        yield return new WaitForSeconds(150000000000000f);
    }
}
