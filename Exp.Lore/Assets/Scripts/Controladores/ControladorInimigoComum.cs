using UnityEngine;
using UnityEngine.AI;

public class ControladorInimigoComum : ControladorInimigos
{
	NavMeshAgent agent;
	public Transform waypointsPai;
	int quantidadeWaypoints;
	GameObject[] listaWaypoints;
	int waypointDestino = 0;
	float distanciaProxWaypoint;

	protected override void Start()
	{
		base.Start();

		agent = GetComponent<NavMeshAgent>();

		quantidadeWaypoints = waypointsPai.childCount;
		listaWaypoints = new GameObject[quantidadeWaypoints];
		for(int i = 0; i < quantidadeWaypoints; i++)
		{
			listaWaypoints[i] = waypointsPai.GetChild(i).gameObject;
		}
	}

	protected override void idle()
	{
		anim.SetBool("perseguindo", false);
		agent.SetDestination(listaWaypoints[waypointDestino].transform.position);
		distanciaProxWaypoint = Vector3.Distance(listaWaypoints[waypointDestino].transform.position, agent.transform.position);
		
		if (distanciaProxWaypoint < 2)
		{
			if (waypointDestino < listaWaypoints.Length - 1)
			{
				waypointDestino++;
			}
			else
			{
				waypointDestino = 0;
			}
		}
	}
	protected override void dentroRaioDeVisao()
	{
		anim.SetBool("perseguindo", true);
		agent.SetDestination(target.position);
	}
	protected override void atacar()
	{
		Invoke("ataque", ataqueDelay);
		anim.SetTrigger("atacar");
		cooldownAtaqueAtual = cooldownAtaqueMax;
	}

	void ataque()//ta sendo usado no Invoke, por isso tem 0 referencias mas está sendo usado sim, mas ele deveria estar sendo chamado pelo StateMachineBehaviour do ataque dele e não desse script assim como o script do ControladorBoss
	{
		if (distancia <= raioDeAtaque + (raioDeAtaque / 5))
			jogadorStats.TomarDano(meusStats.getDano());
	}
}