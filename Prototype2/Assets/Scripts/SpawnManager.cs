using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //animal prefabs
    [SerializeField]private GameObject[] animalPrefabs;
    //x range
    [SerializeField]private float spawnRangeX = 20.0f;
    //z position
    [SerializeField]private float spawnPosZ = 20.0f;
    //Start Delay
    [SerializeField]private float startDelay = 2.0f;
    //Spawn Interval
    [SerializeField]private float spawnInterval = 1.5f;

    //Start is called before the first frame
    void Start()
    {
        InvokeRepeating(spwanAnimalsMethod, startDelay, spawnInterval);
    }
    //Change this string to match the name of the following method
    private const string spwanAnimalsMethod = "SpawnRandomAnimal";
    //Method to spawn animals 
    private void SpawnRandomAnimal()
    {
        // Choose a random number
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        //Spawn Position
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0.0f, spawnPosZ);
        //Spawn Animal
        Instantiate(animalPrefabs[animalIndex], spawnPosition, animalPrefabs[animalIndex].transform.rotation);
    }
}
