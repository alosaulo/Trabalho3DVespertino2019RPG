using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{

    public CapsuleCollider WeaponCollider;

    PlayerController Player;

    private void Start()
    {
        Player = GetComponentInParent<PlayerController>();
    }

    public void ActivateWeaponCollider() {
        Debug.Log("Ativou");
        WeaponCollider.enabled = true;
    }

    public void DeactivateWeaponCollider(){
        Debug.Log("Desativou");
        WeaponCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") {
            NPC npc = other.GetComponent<NPC>();
            npc.SofrerDano(Player.meusAtributos.Forssa);
        }
        if (other.tag == "Spawner") {
            SpawnerController SC = other.GetComponent<SpawnerController>();
            SC.SofrerDano(Player.meusAtributos.Forssa);
        }
    }

}
