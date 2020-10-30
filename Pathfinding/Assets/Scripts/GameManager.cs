using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public int gold;
    public Text goldText;
    public int minerCost;


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
            GameObject explorerGO = Instantiate(explorer, spawnPoint.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            GameObject minerGO = Instantiate(miner, spawnPoint.position, Quaternion.identity);
        }
    }
}
