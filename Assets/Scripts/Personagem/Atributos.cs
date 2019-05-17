using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Atributos
{
    public float vida;
    public float vidaAtual;
    public float mana;
    public float manaAtual;

    public int Inteligencia;
    public int Forssa;
    public int Destreza;

    public ModAtributos ModAtributos;

    public void CalculaVida() {
        vida = Forssa * 5;
        vidaAtual = vida;
    }
    public void CalculaMana() {
        mana = Inteligencia * 5;
        manaAtual = mana;
    }
    public void CalulaModClass() { }

}
