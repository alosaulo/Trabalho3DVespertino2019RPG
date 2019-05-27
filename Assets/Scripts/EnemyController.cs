using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : NPC
{
    Animator meuAnimator;
    Rigidbody meuCorpo;
    NavMeshAgent meuAgente;

    bool atacando = false;

    Player Player;

    bool PlayerDead = false;

    [Header("Script Inimigo")]

    public CapsuleCollider WeaponCollider;

    public float distanciaDoAlvo;

    public float distanciaMinPlayer;

    public float hitDistance;

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

        Waypoints = SpawnManager._instance.Waypoints;

    }

    // Update is called once per frame
    void Update()
    {
        if(morrido == false) { 
            MovimentoAI();
            MudarAnimassaum();
            PlayerDead = Player.estaMorto();
        }
        else{
            estadoPernas = EstadosPersonagem.Morrendo;
            estadoTorso = EstadosPersonagem.Morrendo;
            meuAgente.SetDestination(transform.position);
            MudarAnimassaum();
        }
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
            if (distanciaPlayer > 1.3)
            {
                //Seta o destino para o player
                meuAgente.SetDestination(Player.transform.position);
            }
            else {
                meuAgente.SetDestination(transform.position);
                estadoTorso = EstadosPersonagem.Ataque;
            }
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

    public override void SofrerDano(float dano)
    {
        base.SofrerDano(dano);
        if (morrido == true) {
            meuAnimator.SetTrigger("Morrendo");
            estadoPernas = EstadosPersonagem.Morrendo;
            estadoTorso = EstadosPersonagem.Morrendo;
            GetComponent<CapsuleCollider>().enabled = false;
            Destroy(gameObject, 5);
        }
    }

    /// <summary>
    /// Reseta as animações, volta ao estado de movimento
    /// </summary>
    public void RestearAnimassaum() {
        atacando = false;
        if (estadoPernas != EstadosPersonagem.Movimento) {
            estadoPernas = EstadosPersonagem.Movimento;
        }
        if (estadoTorso != EstadosPersonagem.Movimento) {
            estadoTorso = EstadosPersonagem.Movimento;
        }
    }

    public void AtivarColliderArma() {
        WeaponCollider.enabled = true;
    }

    public void DesativarColliderArma() {
        WeaponCollider.enabled = false;
    }

    public void Atacar() {
        if (atacando == false) {
            atacando = true;
            Debug.Log("Atacando");

            RaycastHit hit;

            Vector3 minhaPos = transform.position;

            float offsetY = minhaPos.y + 0.8f;

            Vector3 originOffset = new Vector3(minhaPos.x,
                offsetY,
                minhaPos.z);

            bool rayHit = Physics.Raycast(minhaPos,
                transform.TransformDirection(Vector3.forward),
                out hit,
                hitDistance);

            Debug.DrawRay(originOffset,
                transform.TransformDirection(Vector3.forward) * hitDistance,
                Color.magenta, 0.001f);

            if (rayHit)
            {
                Debug.Log(gameObject.tag);

                if (hit.collider.gameObject.tag == "Player")
                {
                    Player.SofrerDano(meusAtributos.Forssa);
                }
            }
            atacando = false;
        }
    }

}
