using UnityEngine;
using UnityEngine.AI;

public class ControladorInimigo : MonoBehaviour
{
	public float raioDeVisao = 10f;

	Transform target;
	NavMeshAgent agent;
	PersonagemCombate combate;
	public GameObject[] Waypoints;
	private int WaypointDestino = 0;
    Animator anim;

	// Start is called before the first frame update
	void Start()
    {
		target = Ref_posiçao_jogador.instance.player.transform;
		agent = GetComponent<NavMeshAgent>();
		combate = GetComponent<PersonagemCombate>();
        anim = this.gameObject.GetComponentInChildren<Animator>();

	}

    // Update is called once per frame
    void Update()
    {
        
        anim.SetBool("perseguindo", false);
		float distancia = Vector3.Distance(target.position, transform.position);

		agent.SetDestination(Waypoints[WaypointDestino].transform.position);

		float distancia_inimigo_destino = Vector3.Distance(Waypoints[WaypointDestino].transform.position, agent.transform.position);

		if (distancia_inimigo_destino < 2)
		{
			if (WaypointDestino < Waypoints.Length - 1)
			{
				WaypointDestino++;
			}
			else
			{
				WaypointDestino = 0;
			}

		}



		if (distancia <= raioDeVisao)
		{
            anim.SetBool("perseguindo", true);
            target.LookAt(new Vector3(transform.position.x, target.position.y, transform.position.z));

            agent.SetDestination(target.position);

			if(distancia <= 3)
			{
				JogadorStats alvoStats = target.GetComponent<JogadorStats>();
				if(alvoStats != null)
				{
					combate.Ataque(alvoStats);
                    if (combate.atacando)
                    {
                        anim.SetTrigger("atacar");
                        combate.atacando = false;
                    }
				}
				
				OlharParaAlvo();
			}
		}
    }

	void OlharParaAlvo()
	{
		Vector3 direcao = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direcao.x, 0, direcao.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, raioDeVisao);
	}
}


