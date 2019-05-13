using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Personagem : MonoBehaviour
{
    public int Nivel;
    public int Experiencia;

    public TipoRassa minhaRassa;
    public Classe minhaClasse;
    public Atributos meusAtributos;
    public List<Habilidade> Habilidades;
    public List<Item> Itens;

    protected bool morrido = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void SofrerDano(float dano) {
        meusAtributos.vidaAtual -= dano;
        if (meusAtributos.vidaAtual <= 0) {
            morrido = true;
            Debug.Log("Morreu!");
        }

    }

    public void AtaqueMelee() {

    }

    public void AtaqueDistancia() {

    }

    public void AtaqueEspecial(int opssaum) {

    }

    public void AdicionarExp(int exp) {
        Experiencia += exp;
    }

    public void ModificarAnimassaum() {

    }

}
