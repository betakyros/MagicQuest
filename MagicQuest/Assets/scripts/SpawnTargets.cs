using UnityEngine;
using System.Collections;

//contains information about all enemies in the current level
public class SpawnTargets : MonoBehaviour {

    public GameObject[] targetMagics;
    public Transform spawnPoint;
    public TargetController targetController;
    public AudioClip spawnClip;
    private float spawnRate;
    private int targetMagicsLowerBound;
    private int targetMagicsUpperBound;

    void Start()
    {
        spawnRate = 1.0f;
        targetMagicsLowerBound = 0;
        targetMagicsUpperBound = 2;
    }

    public void setSpawnRate(float newSpawnRate)
    {
        spawnRate = newSpawnRate;
    }

    //each "bounds" should represent a different wave of enemies
    public void setTargetMagicsBounds(int newLowerBound, int newUpperBound) 
    {
        targetMagicsLowerBound = newLowerBound;
        targetMagicsUpperBound = newUpperBound;
    }

    public void SendWave(int numEnemies)
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Invoke("SpawnMagic", spawnRate * i);
        }
    }

    void SpawnMagic()
    {
        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        targetController.IncrementTargets();
        AudioSource.PlayClipAtPoint(spawnClip, Camera.main.transform.position);
        Instantiate(targetMagics[Random.Range(targetMagicsLowerBound, targetMagicsUpperBound+1)], spawnPoint.position, new Quaternion());
    }
}
