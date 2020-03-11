using UnityEngine;

public class InimigoStats : SerVivoStats
{
	public float radius = 3f;
	public Transform interactionTransform;
	
	PersonagemCombate cooldown;
	public float CoolDown;
	Animator anim;

    public AudioSource audioListenerBoss;

    ControladorMissoes controladorMissoes;

    private void Start()
	{
		cooldown = player.GetComponent<PersonagemCombate>();
		float Cooldown = cooldown.CooldownAtaque;
		CoolDown = Cooldown;
		anim = GetComponentInChildren<Animator>();

        controladorMissoes = GameObject.Find("ControladorGeral").GetComponent<ControladorMissoes>();

	}



	void Update()
	{
		float Cooldown = cooldown.CooldownAtaque;
		float distance = Vector3.Distance(player.transform.position, interactionTransform.position);

		if (Input.GetButtonDown("Atacar") && (distance <= radius) && (Cooldown <= 0))
		{
			TomarDano(danoInimigo);

		}
	}


	public override void MorrerAnimacao()
	{
        anim.SetTrigger("Morrer");

        if (this.gameObject.tag == "Boss")
        {
            audioListenerBoss.enabled = false;

			controladorMissoes.matarBoss();
        }
		
		//Destroy(gameObject);
	}

	public override void Morrer()
	{
		Destroy(gameObject);
	}

}
