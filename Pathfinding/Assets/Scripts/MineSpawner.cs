using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    public int maxX;
    public int maxZ;
    public float posY;
    public float timer;
    public float newSpawnTime;

    public int cantSpots;
    public int maxSpots;
    public GameObject flag;
    // Start is called before the first frame update
    void Update()
    {
        if (cantSpots<maxSpots)
        {
            timer += Time.deltaTime;
            if (timer > newSpawnTime)
            {
                Vector3 pos = new Vector3(Random.Range(-maxX, maxX), posY, Random.Range(-maxZ, maxZ));
                GameObject flagGO = Instantiate(flag, pos, Quaternion.identity);
                cantSpots++;
                timer = 0;
            }
        }
    }
}
