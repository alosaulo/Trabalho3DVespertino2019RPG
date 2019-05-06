using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : NPC
{
    Animator meuAnimator;
    Rigidbody meuCorpo;
    NavMeshAgent meuAgente;

    Player Player;

    public float distanciaDoAlvo;

    public float distanciaMinPlayer;

    public List<Transform> Waypoints;

    public float movimentoSpeed;

    public EstadosPersonagem estadoTorso;
    public EstadosPersonagem estadoPernas;

    float velocidadeMax = 12;
    public int waypointAtual = 0;

    // Start is called before the first frame update
    void Start()
    {
        meuAnimator = GetComponent<Animator>();
        meuAgente = GetComponent<NavMeshAgent>();
        meuCorpo = GetComponent<Rigidbody>();

        Player = GameManager._instance.Player;
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

    /// <summary>
    /// Cuida da movimentação da IA do esqueleto
    /// </summary>
    public void MovimentoAI() {

        //Velocidade do navmesh agent
        float velocidadeAtual = meuAgente.velocity.sqrMagnitude;

        //Velocidade entre 0 e 1 para modificar a animação de andar
        movimentoSpeed = velocidadeAtual / velocidadeMax;

        //Verifica se a movimentação é diferente de 0, se for muda os estados para movimento!
        //Que são lidos pelo metodo mudar animassaum.
        if (movimentoSpeed != 0) {
            estadoPernas = EstadosPersonagem.Movimento;
            estadoTorso = EstadosPersonagem.Movimento;
        }

        //Define o destino atual
        Vector3 destino = Waypoints[waypointAtual].position;
        
        //Distancia entre o inimigo e o destino
        float distancia = Vector3.Distance(transform.position, destino);

        //Distancia do inimigo e o player
        float distanciaPlayer = Vector3.Distance(transform.position, Player.transform.position);

        //Verifica se a distancia do waypoint é menor que a do alvo
        //Se for, quer dizer que o inimigo chegou no destino
        if (distancia <= distanciaDoAlvo) {
            //Aumenta o waypoint para 1
            waypointAtual += 1;
            //Se chegar no último waypoint, começa do começo, ou seja 0
            if(waypointAtual >= Waypoints.Count){
                waypointAtual = 0;
            }
        }

        //Verifica se o esqueleto está perto do Player
        //Se não, continua seguindo o waypoint
        if (distanciaPlayer < distanciaMinPlayer)
        {
            //Seta o destino para o player
            meuAgente.SetDestination(Player.transform.position);
            //Desenha a linha vermelha
            Debug.DrawLine(transform.position, Player.transform.position, Color.red);
        }
        else {
            //Seta o destino para o waypoint
            meuAgente.SetDestination(Waypoints[waypointAtual].position);
            //Desenha a linha ciana
            Debug.DrawLine(transform.position, Player.transform.position, Color.cyan);
        }
    }

    /// <summary>
    /// Reseta as animações, volta ao estado de movimento
    /// </summary>
    public void RestearAnimassaum() {
        if (estadoPernas != EstadosPersonagem.Movimento) {
            estadoPernas = EstadosPersonagem.Movimento;
        }
        if (estadoTorso != EstadosPersonagem.Movimento) {
            estadoTorso = EstadosPersonagem.Movimento;
        }
    }

}
