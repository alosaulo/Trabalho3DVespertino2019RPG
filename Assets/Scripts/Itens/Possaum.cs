using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possaum : Item
{

    public TipoPossaum TipoPossaum;

    public Tamanho Tamanho;

    public void SorteiaPossaum(){
        float rand = Random.Range(0f,1f);
        Debug.Log("Valor do drop "+rand);
        if(rand <= 0.8f){
            Destroy(gameObject);
        }else{
            if(rand <= 0.9f){
                Tamanho = Tamanho.Pequeno;
            }else{
                if(rand <= 0.95f){
                    Tamanho = Tamanho.Medio;
                }else{
                    if(rand <= 1f){
                        Tamanho = Tamanho.Grande;
                    }
                }
            }
        }
        
    }

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
