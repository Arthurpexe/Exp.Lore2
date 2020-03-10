using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControladorBoss1 : MonoBehaviour
{
	public float raioDeVisao = 10f;
	public float cooldownAtaque = 0f;
	public float CooldownAtaque = 5f;

	Renderer rend;
	Transform target;
	Boss1Combate combate;
    Animator mecanimBoss;

	void Start()
    {
		target = ControladorPersonagem.instancia.transform;
		combate = GetComponent<Boss1Combate>();
        mecanimBoss = GetComponentInChildren<Animator>();
		rend = GetComponentInChildren<Renderer>();
	}

    void Update()
    {
		cooldownAtaque -= Time.deltaTime;

		float distancia = Vector3.Distance(target.position, transform.position);

		rend.material.color = Color.white;

		if (distancia <= raioDeVisao)
		{
			 
			SerVivoStats alvoStats = target.GetComponent<SerVivoStats>();
			if (alvoStats != null && cooldownAtaque <= 0)
			{
				combate.Ataque(alvoStats);
				
			}
			
			

				OlharParaAlvo();

            mecanimBoss.SetBool("dentroDoRangeVisao", true);
		}
        else
        {
            mecanimBoss.SetBool("dentroDoRangeVisao", false);
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
			


