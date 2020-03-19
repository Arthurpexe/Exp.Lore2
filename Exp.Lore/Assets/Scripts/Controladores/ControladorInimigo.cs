using UnityEngine;
using UnityEngine.AI;

public class ControladorInimigo : MonoBehaviour
{
	public float raioDeVisao = 10;
	public float raioDeAtaque = 3;

	Transform target;

	NavMeshAgent agent;
	public Transform waypointsPai;
	int quantidadeWaypoints;
	GameObject[] listaWaypoints;
	int waypointDestino = 0;
	float distanciaProxWaypoint;

	Animator anim;
	SerVivoStats jogadorStats;
	InimigoStats meusStats;

	float cooldownAtaqueAtual = 0;
	float cooldownAtaqueMax = 2;
	float distancia;
	
	void Start()
	{
		target = ControladorPersonagem.instancia.transform;
		jogadorStats = target.GetComponent<SerVivoStats>();
		meusStats = GetComponent<InimigoStats>();
		agent = GetComponent<NavMeshAgent>();
		anim = this.gameObject.GetComponentInChildren<Animator>();

		quantidadeWaypoints = waypointsPai.childCount;
		listaWaypoints = new GameObject[quantidadeWaypoints];
		for(int i = 0; i < quantidadeWaypoints; i++)
		{
			listaWaypoints[i] = waypointsPai.GetChild(i).gameObject;
		}
	}

	void Update()
	{
		cooldownAtaqueAtual -= Time.deltaTime;
		distancia = Vector3.Distance(target.position, transform.position);

		if (distancia <= raioDeVisao)
		{
			anim.SetBool("perseguindo", true);
			olharParaAlvo();

			agent.SetDestination(target.position);

			if (distancia <= raioDeAtaque && cooldownAtaqueAtual <= 0)
			{
				Invoke("atacar", 0.6f);
				anim.SetTrigger("atacar");
				cooldownAtaqueAtual = cooldownAtaqueMax;
			}
		}
		else
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
	}

	void atacar()
	{
		if (distancia <= raioDeAtaque + (raioDeAtaque / 5))
			jogadorStats.TomarDano(meusStats.getDano());
	}

	void olharParaAlvo()
	{
		Vector3 direcao = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direcao.x, 0, direcao.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, raioDeVisao);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, raioDeAtaque);
	}
}