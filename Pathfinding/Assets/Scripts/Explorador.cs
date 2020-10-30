using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorador : MonoBehaviour
{
    public enum ExploradorStates
    {
        Idle,
        Patrol,
        Marking,
        AllStates
    }

    public ExploradorStates currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case ExploradorStates.Idle:
                break;
            case ExploradorStates.Patrol:
                break;
            case ExploradorStates.Marking:
                break;
            case ExploradorStates.AllStates:
                break;
            default:
                break;
        }
    }
}
