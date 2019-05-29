using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerController : MonoBehaviour
{

    public Image vidaSpawner;

    [HideInInspector]
    public int qtdWaypoints;
    public int vidaTotal;
    public int vidaAtual;
    public List<CaminhosInimigo> Caminhos;

    public GameObject SkeletonEnemy;
    bool enemyReadyToSpawn = true;
    public float secondsToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        qtdWaypoints = Caminhos.Count;
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
        GameObject go = Instantiate(SkeletonEnemy, 
            transform.position, 
            Quaternion.identity);
        go.GetComponent<EnemyController>().meuSpawner = this;
        yield return new WaitForSeconds(secondsToSpawn);
        enemyReadyToSpawn = true;
    }
    public void SofrerDano(int dano) {
        vidaAtual -= dano;
        vidaSpawner.fillAmount = (float)vidaAtual / (float)vidaTotal;
        if (vidaAtual <= 0) {
            Destroy(gameObject);
        }
    }


}
