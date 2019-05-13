using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Personagem
{

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

        //GetWeaponAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        SheathWeapon();
        ChangeAnimations();
    }

    void Move() {
        vAxis = Input.GetAxis("Vertical");

        if (Mathf.Abs(vAxis) > 0)
            myBody.AddRelativeForce(Vector3.forward * vAxis * meusAtributos.Destreza * 10);
        

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

}
