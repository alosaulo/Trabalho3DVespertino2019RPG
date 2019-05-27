using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspecialController : MonoBehaviour
{

    Rigidbody myBody;
    public float speed;

    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();
        myBody.AddRelativeForce(Vector3.forward * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDamage(float damage) {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") {
            EnemyController Enemy = other.GetComponent<EnemyController>();
            Enemy.SofrerDano(damage);
        }
    }

}
