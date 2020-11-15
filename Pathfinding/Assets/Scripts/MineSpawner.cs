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
    public Vector2 baseRadius;

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

                while ((pos.x < -baseRadius.x && pos.x < baseRadius.x) || (maxZ > -baseRadius.y && maxZ < baseRadius.y))
                {
                    pos.x = Random.Range(-maxX, maxX);
                    pos.x = Random.Range(-maxZ, maxZ);
                }

                GameObject flagGO = Instantiate(flag, pos, Quaternion.identity);
                cantSpots++;
                timer = 0;
            }
        }
    }
}
