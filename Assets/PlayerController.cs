using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
{
    public GUIPlayer GUIPlayer;
    public CapsuleCollider WeaponCollider;
    public GameObject playerHand;
    public GameObject playerScabbard;
    public GameObject playerWeapon;
    public Transform transformPe;
    Rigidbody myBody;
    Animator myAnimator;
    public bool sheath = false;
    public bool isGrounded = false;
    float vAxis, hAxis;

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
            Attack();
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
