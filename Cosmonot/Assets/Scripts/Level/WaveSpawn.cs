using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawn : MonoBehaviour{
    public CircleCollider2D playerRadiusCollider;
    public List<MobPercentage> enemies;

    public void SpawnWave(int waveSize){
        for (int x = 0; x < waveSize; x++) {
            var random = Random.Range(0, 100);
            int lowLim;
            int Lim = 0;
            for (int i = 0; i < enemies.Count; i++) {
                lowLim = Lim;
                Lim += enemies[i].percentage;
                if (random >= lowLim && random < Lim) {
                    Instantiate(enemies[i].prefab, Random.insideUnitCircle * playerRadiusCollider.radius, Quaternion.identity);
                }
            }
        }
    }
}

[System.Serializable]
public class MobPercentage{
    [Range(1, 100)]
    public int percentage = 1;
    public GameObject prefab;
}
