using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoStats : PersonagemStats
{
	public float radius = 3f;
	public Transform interactionTransform;
	
	PersonagemCombate cooldown;
	public float CoolDown;
	Animator anim;

    public AudioSource audioListenerBoss;

    ControladorPersonagem controladorPersonagem;

    private void Start()
	{
		cooldown = player.GetComponent<PersonagemCombate>();
		float Cooldown = cooldown.CooldownAtaque;
		CoolDown = Cooldown;
		anim = GetComponentInChildren<Animator>();

        controladorPersonagem = ControladorPersonagem.instancia;

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


	public override void MorrerAnimaçao()
	{
        anim.SetTrigger("Morrer");

        if (this.gameObject.tag == "Boss")
        {
            audioListenerBoss.enabled = false;

            for (int i = 0; i < controladorPersonagem.missoes.Length; i++)
            {
                if (controladorPersonagem.missoes[i].titulo == "A Hora da Verdade")
                {
                    controladorPersonagem.ouro = controladorPersonagem.missoes[i].recompensaOuro;
                    controladorPersonagem.missoes[i].missaoConcluida();
                    controladorPersonagem.mudouMissao();
                }
                
            }
        }

		
		//Destroy(gameObject);
	}

	public override void Morrer()
	{
		Destroy(gameObject);
	}

}
