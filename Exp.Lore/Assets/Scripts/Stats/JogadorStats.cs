using UnityEngine;

public class JogadorStats : SerVivoStats
{
    void Start()
    {
		ControladorEquipamento.instance.trocaDeEquipamento += trocaDeEquipamento;
    }
    
	void trocaDeEquipamento (Equipamento novoItem, Equipamento velhoItem)
	{
		if(novoItem != null)
		{
			armadura += novoItem.getArmaduraModificador();
			dano += novoItem.getDanoModificador();
			vidaMaxima += novoItem.getVidaMaximaModificador();
		}

		if(velhoItem != null)
		{
			armadura -= velhoItem.getArmaduraModificador();
			dano -= velhoItem.getDanoModificador();
			vidaMaxima -= novoItem.getVidaMaximaModificador();
		}
	}

	public override void MorrerAnimacao()
	{
		anim.SetTrigger("morto");
	}
}
