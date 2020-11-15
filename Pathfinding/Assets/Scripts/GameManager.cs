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

    public GameObject infoGold;

    private float timer;
    private float maxInfoGoldTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = "Gold: " + gold;

        if (infoGold)
        {
            timer += Time.deltaTime;
            if (timer >= maxInfoGoldTime)
            {
                infoGold.SetActive(false);
                timer = 0;
            }
        }
    }

    public void CreateExplorer()
    {
        if (gold >= explorerCost)
        {
            gold -= explorerCost;
            GameObject explorerGO = Instantiate(explorer, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            infoGold.SetActive(true);
        }
    }

    public void CreateMiner()
    {
        if (gold >= minerCost)
        {
            gold -= minerCost;
            GameObject minerGO = Instantiate(miner, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            infoGold.SetActive(true);
        }
    }
}