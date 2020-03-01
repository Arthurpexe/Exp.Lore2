using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PersonagemStats))]
public class PersonagemCombate : MonoBehaviour
{
	public float velocidadeAtaque = 1f;
	public float cooldownAtaque = 0f;
	public float CooldownAtaque;
	public float ataqueDelay = .6f;
	PersonagemStats playerStats;
	GameObject player;
	float distancia;
	

    public bool atacando;


    PersonagemStats myStats;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerStats = player.GetComponent<PersonagemStats>();

		myStats = GetComponent<PersonagemStats>();
		
	}


	void Update()
	{
		cooldownAtaque -= Time.deltaTime;
		distancia = Vector3.Distance(player.transform.position, transform.position);


	}

	public void Ataque(PersonagemStats alvoStats)
	{
		

		if(cooldownAtaque <= 0)
		{
            atacando = true;
            StartCoroutine(DarDano(alvoStats, ataqueDelay));
			cooldownAtaque = CooldownAtaque;
            

		}
	}

	IEnumerator DarDano(PersonagemStats stats, float delay) // Atraso para dano acontecer apenas no final da animação de ataque, ao invez de imediatamente.
	{
		yield return new WaitForSeconds(delay);

		if (distancia <= 3.5)
		{
			stats.TomarDano(myStats.dano - playerStats.armadura);
		}

	}
}
