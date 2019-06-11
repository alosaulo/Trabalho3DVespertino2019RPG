using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Personagem
{
    public GUIPlayer GUIPlayer;
    public CapsuleCollider WeaponCollider;

    public GameObject OrigemAtaqueEspecial;
    public GameObject AtkSpecial;
    public GameObject playerHand;
    public GameObject playerScabbard;
    public GameObject playerWeapon;

    public Transform transformPe;

    Rigidbody myBody;
    Animator myAnimator;

    public bool sheath = false;
    public bool isGrounded = false;
    float vAxis, hAxis;

    bool manaRecovery = true;
    int lastLevel;

    public int specialAtkMana;
    public int manaToRecover;
    public int secondsToManaRecover;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody>();
        Debug.Log(meusAtributos.Destreza);

        meusAtributos.CalculaVida();
        meusAtributos.CalculaMana();

        GUIPlayer.SetVida(meusAtributos.vidaAtual, meusAtributos.vida);
        GUIPlayer.SetMana(meusAtributos.manaAtual, meusAtributos.mana);

        GetWeaponAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        if (morrido == false)
        {
            Move();
            Jump();
            if (manaRecovery == true) {
                StartCoroutine("ManaRecovery");
            }
            Attack();
            AttackSpecial();
            SheathWeapon();
            CheckGround();
        }
        ChangeAnimations();
    }

    void Move() {
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");

        if (Mathf.Abs(vAxis) > 0)
            myBody.AddRelativeForce(Vector3.forward * vAxis * meusAtributos.Destreza * 10);
        if (Mathf.Abs(hAxis) > 0)
            myBody.AddTorque(Vector3.up * hAxis);

    }

    void CheckGround() {
        float radius = 0.02f;
        float distance = 0.1f;
        var grounded = Physics.SphereCast(new Ray(transformPe.position, Vector3.down), 
            radius, 
            distance);
        if (grounded == true)
        {
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transformPe.position, 0.02f);
    }

    void Jump() {
        if (Input.GetButtonUp("Jump") && isGrounded == true) {
            myBody.AddForce(Vector3.up * 300);
        }
    }

    void Attack() {
        if (Input.GetButtonDown("Fire1") && sheath == true) {
            myAnimator.SetTrigger("Ataque");
        }
    }

    void AttackSpecial() {
        if (Input.GetButtonDown("Fire2") && sheath == true && 
            meusAtributos.manaAtual >= specialAtkMana) {
            myAnimator.SetTrigger("AtaqueEspecial");
        }
    }

    void DoAttackSpecial() {

        GameObject atk = Instantiate(AtkSpecial, 
            OrigemAtaqueEspecial.transform.position, 
            OrigemAtaqueEspecial.transform.rotation);

        EspecialController esp = atk.GetComponent<EspecialController>();

        esp.SetDamage(meusAtributos.Forssa * 1.5f);

        ManaWaste(specialAtkMana);

        Destroy(atk, 0.3f);

    }

    void ManaWaste(int mana) {
        if(meusAtributos.manaAtual > 0)
            meusAtributos.manaAtual -= mana;

        GUIPlayer.SetMana(meusAtributos.manaAtual,
            meusAtributos.mana);
    }

    IEnumerator ManaRecovery() {
        if(meusAtributos.manaAtual < meusAtributos.mana)
        {
            meusAtributos.manaAtual += manaToRecover;
            if (meusAtributos.manaAtual > meusAtributos.mana) {
                meusAtributos.manaAtual = meusAtributos.mana;
            }
            GUIPlayer.SetMana(meusAtributos.manaAtual,
                meusAtributos.mana);
        }
        manaRecovery = false;
        yield return new WaitForSeconds(secondsToManaRecover);
        manaRecovery = true;
    }

    void SheathWeapon() {
        if (Input.GetButtonDown("Sheath")) {
            Debug.Log("Apertei o butão");
            if (sheath == true)
            {
                sheath = false;
            }
            else
            {
                sheath = true;
            }
        }
    }

    void GetWeaponAnimation() {
        if (sheath == true)
        {
            playerWeapon.transform.SetParent(playerHand.transform);
        }
        else
        {
            playerWeapon.transform.SetParent(playerScabbard.transform);
        }
    }

    void ChangeAnimations() {
        myAnimator.SetFloat("Y", vAxis);
        myAnimator.SetBool("Desembainhar",sheath);
        myAnimator.SetBool("Pulo", !isGrounded);
    }

    public void ActivateWeaponCollider()
    {
        Debug.Log("Ativou");
        WeaponCollider.enabled = true;
    }

    public void DeactivateWeaponCollider()
    {
        Debug.Log("Desativou");
        WeaponCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.SofrerDano(meusAtributos.Forssa);
        }
        if(other.tag == "Possaum"){
            Possaum possaum = other.GetComponent<Possaum>();
            possaum.RecuperarVida(meusAtributos, GUIPlayer);
        }
    }

    public void SetExperiencia(int exp){
        AdicionarExp(exp);
        if(Experiencia < 100){
            GUIPlayer.SetExp(Experiencia,100);
            lastLevel = 1;
            Nivel = 1;
        }else{
            if(Experiencia < 300){
                GUIPlayer.SetExp(Experiencia,300);
                if(lastLevel == 1){
                    meusAtributos.qtdPontos += 5;
                    lastLevel = 2;
                }
                Nivel = 2;
            }else{
                if(Experiencia < 600){
                    GUIPlayer.SetExp(Experiencia,600);
                    if(lastLevel == 2){
                        meusAtributos.qtdPontos += 5;
                        lastLevel = 3;
                    }
                    Nivel = 3;
                }else{
                    if(Experiencia < 1000)
                    GUIPlayer.SetExp(Experiencia,1000);
                    if(lastLevel == 3){
                        meusAtributos.qtdPontos += 5;
                        lastLevel = 4;
                    }
                    Nivel = 4;
                }
            }
        }

        GUIPlayer.SetLvl(Nivel);
    }
    
    public override void SofrerDano(float dano)
    {
        base.SofrerDano(dano);
        GUIPlayer.SetVida(meusAtributos.vidaAtual, meusAtributos.vida);
        myAnimator.SetTrigger("Dano");
        if (morrido == true)
        {
            myAnimator.SetTrigger("Morte");
            GetComponent<CapsuleCollider>().enabled = false;
            myBody.useGravity = false;
        }
    }

}
