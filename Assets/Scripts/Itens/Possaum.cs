using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possaum : Item
{
    public TipoPossaum TipoPossaum;
    public Tamanho Tamanho;
    public void RecuperarVida(Atributos atributos, GUIPlayer gui) { 
        if(atributos.VidaAtualMenorqVida()){
            switch(Tamanho){
                case Tamanho.Pequeno:
                    atributos.RecuperarVida(1.15f);
                    gui.SetVida(atributos.vidaAtual, atributos.vida);
                break;
                case Tamanho.Medio:
                    atributos.RecuperarVida(1.30f);
                    gui.SetVida(atributos.vidaAtual, atributos.vida);
                break;
                case Tamanho.Grande:
                    atributos.RecuperarVida(1.50f);
                    gui.SetVida(atributos.vidaAtual, atributos.vida);
                break;
            }
        }
        Destroy(gameObject);
    }
    public void RecuperarMana() { }
}
