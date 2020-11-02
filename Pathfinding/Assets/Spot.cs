using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{
    public Explorador explorador;
    public GameObject goldMine;

    private void Awake()
    {
        explorador = GetComponentInParent<Explorador>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Flag")
        {
            explorador.sight.mineInSight = false;
            Destroy(other.gameObject);
            GameObject goldMineGO = Instantiate(goldMine, other.transform.position, Quaternion.identity);
            explorador.currentState = Explorador.ExploradorStates.Patrol;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Explorer")
        //{
        //    Destroy(this.gameObject);
        //}
    }
}
