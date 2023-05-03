using UnityEngine;
using TMPro;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public TextMeshProUGUI waveCountText;
    private int waveCount = 0;

    public void PublicSpawnWave() {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave() {
        Debug.Log("Wave " + waveCount + " starting to spawn!");
        waveCount++;
        waveCountText.text = waveCount.ToString();

        for (int i = 0; i <= waveCount - 1; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(0.42f);
        }
    }

    void SpawnEnemy () {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
