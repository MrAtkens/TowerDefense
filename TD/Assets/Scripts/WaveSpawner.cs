using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform enemyPrefab;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float timeBetweenWaves;
    
    private float countdownTimer;
    
    private int waveNumber=1;

    [SerializeField]
    private Text countdownText;
    void Start()
    {
        countdownTimer=timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if(countdownTimer <= 0){
            StartCoroutine(SpawnWave());
            countdownTimer = timeBetweenWaves;
        }

        countdownTimer -= Time.deltaTime;
        countdownText.text= countdownTimer.ToString("0");
    }
    
    private IEnumerator SpawnWave(){
        waveNumber++;
        for(int i = 0; i < waveNumber; i++){
            SpawnEnemy();
            yield return new WaitForSeconds(1.5f);
        }
    }

    private void SpawnEnemy(){
        // создаём противника в определённой точке на карте 
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
