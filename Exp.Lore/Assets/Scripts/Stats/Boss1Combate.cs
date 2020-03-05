using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SerVivoStats))]
public class Boss1Combate : MonoBehaviour
{
	public float velocidadeAtaque = 1f;
	private float cooldownAtaque = 0f;
	public float CooldownAtaque = 5f;
	public float ataqueDelay = .6f;
	private Rigidbody player;
	RaycastHit alvo;
	public float delayAnimaçaoDeAviso = 0f;
	float delayAnimaçaoAtual = 0f;
	Renderer rend;
	GameObject jogador;
	SerVivoStats playerStats;
	Animator mecanimBoss;




	public SerVivoStats myStats;

	void Start()
	{
		player = GetComponent<Rigidbody>();
		playerStats = player.GetComponent<SerVivoStats>();
		myStats = GetComponent<SerVivoStats>();
		jogador = GameObject.FindGameObjectWithTag("Player");
		rend = jogador.GetComponentInChildren<Renderer>();
		mecanimBoss = GetComponentInChildren<Animator>();

	}


	void Update()
	{
		cooldownAtaque -= Time.deltaTime;
		delayAnimaçaoAtual = delayAnimaçaoDeAviso;
		

	}



	public void Ataque(SerVivoStats alvoStats)
	{

		if (cooldownAtaque <= 0)
		{
			Debug.Log("Vou atacar!");

			mecanimBoss.SetTrigger("atacar");
			cooldownAtaque = CooldownAtaque;

		}

	}



	public void Disparar(SerVivoStats alvoStats)
	{
		Physics.SphereCast(transform.position + Vector3.down * 2, 1, transform.forward * 10, out alvo);
		if (alvo.transform.name == "Personagem")
		{
			DarDano(alvoStats);
			cooldownAtaque = CooldownAtaque;
			rend.material.color = Color.red;

		}

		
		
			
		

	}


	public void DarDano(SerVivoStats stats)
	{

		stats.TomarDano(myStats.dano - playerStats.armadura);
		rend.material.color = Color.red;
	}



}			



















	

	


	





