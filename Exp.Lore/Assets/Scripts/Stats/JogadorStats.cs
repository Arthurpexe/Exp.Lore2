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
			aumentarArmadura(novoItem.getArmaduraModificador());
			aumentarDano(novoItem.getDanoModificador());
			aumentarVidaMaxima(novoItem.getVidaMaximaModificador());
		}

		if(velhoItem != null)
		{
			aumentarArmadura(-velhoItem.getArmaduraModificador());
			aumentarDano(-velhoItem.getDanoModificador());
			aumentarVidaMaxima(-velhoItem.getVidaMaximaModificador());
		}
	}

	public override void MorrerAnimacao()
	{
		anim.SetTrigger("morto");
	}
}
