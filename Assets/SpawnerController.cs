using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerController : MonoBehaviour
{

    public Image vidaSpawner;

    public int vidaTotal;
    public int vidaAtual;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SofrerDano(int dano) {
        vidaAtual -= dano;
        vidaSpawner.fillAmount = (float)vidaAtual / (float)vidaTotal;
        if (vidaAtual <= 0) {
            Destroy(gameObject);
        }
    }


}
