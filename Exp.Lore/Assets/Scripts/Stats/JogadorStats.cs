using UnityEngine;

public class JogadorStats : SerVivoStats
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


	public override void MorrerAnimacao()
	{
		anim.SetTrigger("morto");

	}


	public override void Morrer()
	{
		
		anim.SetTrigger("morto");
		
		
	}
}
