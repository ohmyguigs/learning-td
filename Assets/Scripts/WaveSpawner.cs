using UnityEngine;
using TMPro;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public TextMeshProUGUI waveCountText;
    private int waveCount = 0;
    private bool isSpawning = false;

    public void PublicSpawnWave() {
        if (isSpawning) {
            return;
        }
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave() {
        Debug.Log("Wave " + waveCount + " starting to spawn!");
        isSpawning = true;
        waveCount++;
        waveCountText.text = waveCount.ToString();

        for (int i = 0; i <= waveCount - 1; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(0.42f);
        }
        Debug.Log("Wave " + (waveCount-1) + " finished spawning!");
        isSpawning = false;
    }

    void SpawnEnemy () {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
