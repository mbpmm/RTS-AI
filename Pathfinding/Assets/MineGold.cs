using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineGold : MonoBehaviour
{
    public float currentMineGold;

    private void Update()
    {
        if (currentMineGold <= 0)
        {
            Destroy(gameObject);
        }
    }
}
