using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
{
    public CapsuleCollider WeaponCollider;
    public GameObject playerHand;
    public GameObject playerScabbard;
    public GameObject playerWeapon;
    Rigidbody myBody;
    Animator myAnimator;
    public bool sheath = false;
    float vAxis, hAxis;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody>();
        Debug.Log(meusAtributos.Destreza);

        GetWeaponAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        SheathWeapon();
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

    void Attack() {
        if (Input.GetButtonDown("Fire1")) {
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

}
