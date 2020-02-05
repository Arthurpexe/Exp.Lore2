using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorEquipamento : MonoBehaviour
{
	#region Singleton

	public static ControladorEquipamento instance;

	 void Awake()
	 {
		instance = this;
	 }

	#endregion


	public Equipamento[] equipamentoAtual;

    public delegate void AtualizarEquipamento();
    public AtualizarEquipamento atualizarEquipamento;

    public delegate void TrocaDeEquipamento(Equipamento itemNovo, Equipamento itemAntigo);
	public TrocaDeEquipamento trocaDeEquipamento;
	Inventario inventario;

	 void Start()
	 {
		inventario = Inventario.instance;

		int numSlots = System.Enum.GetNames(typeof(EquipamentoSlot)).Length;
		equipamentoAtual = new Equipamento[numSlots];
	 }

	public void Equipar(Equipamento newItem)
	{
		int slotIndex = (int)newItem.equiparSlot;

		Equipamento itemAntigo = null;

		if (equipamentoAtual[slotIndex] != null)
		{
			itemAntigo = equipamentoAtual[slotIndex];
			inventario.Add(itemAntigo);
		}

		if (trocaDeEquipamento != null)
		{
			trocaDeEquipamento.Invoke(newItem, itemAntigo);
		}

		equipamentoAtual[slotIndex] = newItem;
        atualizarEquipamento.Invoke();
	}

	public void Desequipar(int slotIndex) // mudar slotIndex para varivael "Selecionado"
	{
		if(equipamentoAtual[slotIndex] != null) 
		{
			Equipamento itemAntigo = equipamentoAtual[slotIndex];
			inventario.Add(itemAntigo);

			equipamentoAtual[slotIndex] = null;

			if (trocaDeEquipamento != null)
			{
				trocaDeEquipamento.Invoke(null, itemAntigo);
			}
            atualizarEquipamento.Invoke();
        }
	}
}
