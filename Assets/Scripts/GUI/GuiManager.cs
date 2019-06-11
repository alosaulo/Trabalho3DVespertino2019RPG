using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    [Header("Gui Experiência")]
    public Image imgExp;
    public Text txtLvl;
    public Text txtExp;

    [Header("Atributos GUI")]
    public int txtPontos;
    public int txtFor;
    public int txtDex;
    public int txtInt;
    public int txtVida;
    public int txtMana;

    [Header("Player")]
    public PlayerController player;

    public static GuiManager _instance;

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
        
    }

    public void CalcularVida(){}
    public void CalcularMana(){}
    public void AddAttr(int id){}
    public void RemAttr(int id){}
    public void ClickOk(){}
    public void UpdateStatus(){}
    public void Validar(){}

}
