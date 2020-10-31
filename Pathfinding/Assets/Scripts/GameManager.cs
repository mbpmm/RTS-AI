using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public int gold;
    public Text goldText;
    public int minerCost;
    public int explorerCost;


    public GameObject miner;
    public GameObject explorer;
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = "Gold: " + gold;

        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (gold>=explorerCost)
            {
                gold -= explorerCost;
                GameObject explorerGO = Instantiate(explorer, spawnPoint.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("No hay oro suficiente");
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (gold >= explorerCost)
            {
                gold -= explorerCost;
                GameObject minerGO = Instantiate(miner, spawnPoint.position, Quaternion.identity);
            }
            else
            {
                Debug.Log("No hay oro suficiente");
            }
        }
    }
}
