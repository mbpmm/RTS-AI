using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{
    public Explorador explorador;
    public GameObject goldMine;
    public GameObject mineSpawner;

    private MineSpawner mS;

    private void Start()
    {
        explorador = GetComponentInParent<Explorador>();
        mineSpawner = GameObject.FindGameObjectWithTag("MineSpawner");
        mS = mineSpawner.GetComponent<MineSpawner>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Flag")
        {
            explorador.sight.spotInSight = false;
            Destroy(other.gameObject);
            mS.cantSpots--;
            GameObject goldMineGO = Instantiate(goldMine, other.transform.position, Quaternion.identity);
            explorador.currentState = Explorador.ExploradorStates.Patrol;
        }
    }
}
