using System.Collections;
using UnityEngine;

public class JogadorStats : PersonagemStats
{
    static Animator anim;
    // Start is called before the first frame update
    void Start()
    {
		ControladorEquipamento.instance.trocaDeEquipamento += TrocaDeEquipamento;
        anim = GetComponentInChildren<Animator>();
    }

    
	void TrocaDeEquipamento (Equipamento novoItem, Equipamento velhoItem)
	{
		if(novoItem != null)
		{
			armadura = armadura + novoItem.armaduraModificador;
			dano = dano + novoItem.danoModificador;

		}

		if(velhoItem != null)
		{
			armadura = armadura - velhoItem.armaduraModificador;
			dano = dano - velhoItem.danoModificador;
		}
	}


	public override void MorrerAnimaçao()
	{
		anim.SetTrigger("morto");

	}


	public override void Morrer()
	{
		
		anim.SetTrigger("morto");
		
		
	}
}
