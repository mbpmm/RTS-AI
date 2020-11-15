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

    public void Mined()
    {
        Vector3 scaleChange = new Vector3(0.2f,0.2f,0.2f);
        transform.localScale -= scaleChange;
    }
}
