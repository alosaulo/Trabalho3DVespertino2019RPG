using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Atributos
{
    public int qtdPontos;
    
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

    public bool VidaAtualMenorqVida(){
        if(vidaAtual < vida){
            return true;
        }
        return false;
    }
    
    public void RecuperarVida(float valor){
        vidaAtual = vidaAtual * valor;
        if(vidaAtual > vida){
            vidaAtual = vida;
        }
    }

}
