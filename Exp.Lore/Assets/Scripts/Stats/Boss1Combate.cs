using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PersonagemStats))]
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
	PersonagemStats playerStats;
	Animator mecanimBoss;




	public PersonagemStats myStats;

	void Start()
	{
		player = GetComponent<Rigidbody>();
		playerStats = player.GetComponent<PersonagemStats>();
		myStats = GetComponent<PersonagemStats>();
		jogador = GameObject.FindGameObjectWithTag("Player");
		rend = jogador.GetComponentInChildren<Renderer>();
		mecanimBoss = GetComponentInChildren<Animator>();

	}


	void Update()
	{
		cooldownAtaque -= Time.deltaTime;
		delayAnimaçaoAtual = delayAnimaçaoDeAviso;
		

	}



	public void Ataque(PersonagemStats alvoStats)
	{

		if (cooldownAtaque <= 0)
		{
			Debug.Log("Vou atacar!");

			mecanimBoss.SetTrigger("atacar");
			cooldownAtaque = CooldownAtaque;

		}

	}



	public void Disparar(PersonagemStats alvoStats)
	{
		Physics.SphereCast(transform.position + Vector3.down * 2, 1, transform.forward * 10, out alvo);
		if (alvo.transform.name == "Personagem")
		{
			DarDano(alvoStats);
			cooldownAtaque = CooldownAtaque;
			rend.material.color = Color.red;

		}

		
		
			
		

	}


	public void DarDano(PersonagemStats stats)
	{

		stats.TomarDano(myStats.dano - playerStats.armadura);
		rend.material.color = Color.red;
	}



}			



















	

	


	





