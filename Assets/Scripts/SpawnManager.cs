using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float repeatRate = 3.0f;
    public GameObject enemyPrefab;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find(nameof(GameManager)).GetComponent<GameManager>();
        InvokeRepeating(nameof(InstanceEnemy), 1, repeatRate);
    }

    public void InstanceEnemy()
    {
        if(gameManager.isGameActive)
        {
			GameObject player = GameObject.Find("Player");
			Instantiate(enemyPrefab, new Vector3(player.transform.position.x, enemyPrefab.transform.position.y), enemyPrefab.transform.rotation);
		}
       
    }
}
