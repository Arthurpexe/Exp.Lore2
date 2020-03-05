using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SerVivoStats))]
public class PersonagemCombate : MonoBehaviour
{
	public float velocidadeAtaque = 1f;
	public float cooldownAtaque = 0f;
	public float CooldownAtaque;
	public float ataqueDelay = .6f;
	SerVivoStats playerStats;
	GameObject player;
	float distancia;
	

    public bool atacando;


    SerVivoStats myStats;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerStats = player.GetComponent<SerVivoStats>();

		myStats = GetComponent<SerVivoStats>();
		
	}


	void Update()
	{
		cooldownAtaque -= Time.deltaTime;
		distancia = Vector3.Distance(player.transform.position, transform.position);


	}

	public void Ataque(SerVivoStats alvoStats)
	{
		

		if(cooldownAtaque <= 0)
		{
            atacando = true;
            StartCoroutine(DarDano(alvoStats, ataqueDelay));
			cooldownAtaque = CooldownAtaque;
            

		}
	}

	IEnumerator DarDano(SerVivoStats stats, float delay) // Atraso para dano acontecer apenas no final da animação de ataque, ao invez de imediatamente.
	{
		yield return new WaitForSeconds(delay);

		if (distancia <= 3.5)
		{
			stats.TomarDano(myStats.dano - playerStats.armadura);
		}

	}
}
