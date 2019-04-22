using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Habilidade
{
    public string Nome;
    public string Descrissaum;
    public float Custo;

    public float CalcularDano(int atributo) {
        return 0f;
    }

}
