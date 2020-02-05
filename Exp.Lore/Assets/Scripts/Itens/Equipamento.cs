using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Novo Equipamento", menuName = "Inventário/Equipamento")]
public class Equipamento : Item
{
	public EquipamentoSlot equiparSlot;

	public int armaduraModificador;
	public int danoModificador;

	public override void Use()
	{
		
		ControladorEquipamento.instance.Equipar(this);
        //colocar noa aba equipamentos no slot referente ao enum EquipamentoSlot
        RemoverDoInventario();
    }

    public override void desequipar()
    {
        base.desequipar();
        ControladorEquipamento.instance.Desequipar((int)this.equiparSlot);
    }
}

public enum EquipamentoSlot { Capacete, Peitoral, Pernas, Pés}
