using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : NPC
{
    Animator meuAnimator;
    NavMeshAgent meuAgente;

    public List<Transform> Waypoints;

    public float movimentoSpeed;

    public EstadosPersonagem estadoTorso;
    public EstadosPersonagem estadoPernas;

    // Start is called before the first frame update
    void Start()
    {
        meuAnimator = GetComponent<Animator>();
        meuAgente = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        MovimentoAI();
        MudarAnimassaum();
    }

    /// <summary>
    /// Muda a animação conforme o estado do personagem
    /// </summary>
    public void MudarAnimassaum() {
        meuAnimator.SetFloat("Movimento",
            movimentoSpeed);
        meuAnimator.SetInteger("estadoBracos",
            (int)estadoTorso);
        meuAnimator.SetInteger("estadoPernas",
            (int)estadoPernas);
    }

    public void MovimentoAI() {
        meuAgente.SetDestination(Waypoints[0].position);
    }

    public void RestearAnimassaum() {
        if (estadoPernas != EstadosPersonagem.Movimento) {
            estadoPernas = EstadosPersonagem.Movimento;
        }
        if (estadoTorso != EstadosPersonagem.Movimento) {
            estadoTorso = EstadosPersonagem.Movimento;
        }
    }

}
