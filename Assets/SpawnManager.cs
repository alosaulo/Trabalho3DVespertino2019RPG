using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public List<GameObject> Spawners;

    public List<Transform> Waypoints;

    public GameObject SkeletonEnemy;

    public float secondsToSpawn;

    public static SpawnManager _instance;

    bool enemyReadyToSpawn = true;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyReadyToSpawn == true) {
            StartCoroutine("SpawnEnemy");
        }
    }

    IEnumerator SpawnEnemy() {
        enemyReadyToSpawn = false;
        Instantiate(SkeletonEnemy, Spawners[0].transform.position, Quaternion.identity);
        yield return new WaitForSeconds(secondsToSpawn);
        enemyReadyToSpawn = true;
    }

}
