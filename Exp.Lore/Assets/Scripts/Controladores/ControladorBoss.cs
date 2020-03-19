using UnityEngine;

public class ControladorBoss : MonoBehaviour
{
	public float raioDeVisao = 30;
	public float raioDeAtaque = 25;

	Transform target;
	RaycastHit alvo;
	float distancia;
	Animator mecanimBoss;

	public float velocidadeAtaque = 1;
	float cooldownAtaqueAtual = 0;
	public float cooldownAtaqueMax = 5;
	public float ataqueDelay = .6f;

	BossStats meusStats;
	SerVivoStats jogadorStats;

	void Start()
	{
		meusStats = GetComponent<BossStats>();
		jogadorStats = ControladorPersonagem.instancia.getSerVivoStats();
		target = ControladorPersonagem.instancia.transform; ;
		mecanimBoss = GetComponentInChildren<Animator>();
	}

	void Update()
	{
		cooldownAtaqueAtual -= Time.deltaTime;
		distancia = Vector3.Distance(target.position, transform.position);

		if(distancia <= raioDeVisao)
		{
			mecanimBoss.SetBool("dentroDoRangeVisao", true);
			olharParaAlvo();
			if (distancia <= raioDeAtaque)
			{
				atacar();
				// n sei se ta certo, preciso de checar os scripts das animações pra ter ctz, pode ter alguma coisa la q eu ainda nem vi
			}
		}
		
	}

	void olharParaAlvo()
	{
		Vector3 direcao = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direcao.x, 0, direcao.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	void atacar()
	{
		if (cooldownAtaqueAtual <= 0)
		{
			Debug.Log("Vou atacar!");

			mecanimBoss.SetTrigger("atacar");
			cooldownAtaqueAtual = cooldownAtaqueMax;
		}
	}

	public void disparar(SerVivoStats alvoStats)
	{
		Physics.SphereCast(transform.position + Vector3.down * 2, 1, transform.forward * 10, out alvo);
		if (alvo.transform.name == "Personagem")
		{
			alvoStats.TomarDano(meusStats.getDano());
			cooldownAtaqueAtual = cooldownAtaqueMax;
		}
	}
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;//tudo que ta pintado de azul é pra representar a visão do inimigo, mas não tá implementado, apenas desenhado
		Gizmos.DrawRay(transform.position, Vector3.forward * raioDeVisao * 1.15f);
		Gizmos.DrawRay(transform.position, new Vector3(0.7f, 0, 0.7f) * raioDeVisao * 1.15f);
		Gizmos.DrawRay(transform.position, new Vector3(-0.7f, 0, 0.7f) * raioDeVisao * 1.15f);
		Gizmos.DrawRay(transform.position, new Vector3(0.5f, 0, 0.86f) * raioDeVisao * 1.15f);
		Gizmos.DrawRay(transform.position, new Vector3(-0.5f, 0, 0.86f) * raioDeVisao * 1.15f);
		Gizmos.DrawRay(transform.position, new Vector3(0.25f, 0, 0.96f) * raioDeVisao * 1.15f);
		Gizmos.DrawRay(transform.position, new Vector3(-0.25f, 0, 0.96f) * raioDeVisao * 1.15f);

		Gizmos.color = Color.yellow;//amarelo é a atual visão implementada do inimigo, que deveria ser an verdade apenas a audição dele
		Gizmos.DrawWireSphere(transform.position, raioDeVisao);

		Gizmos.color = Color.red;//vermelhor é o raio de ataque do inimigo, apartir dali ele consegue atacar o personagem
		Gizmos.DrawWireSphere(transform.position, raioDeAtaque);

		Gizmos.color = Color.green;// verde representa a mira de ataque do inimigo
		Gizmos.DrawRay(transform.position, transform.forward * raioDeAtaque);
	}
}