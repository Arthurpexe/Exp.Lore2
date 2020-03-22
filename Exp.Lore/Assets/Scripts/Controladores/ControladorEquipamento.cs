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


	Equipamento[] equipamentoAtual;
	Inventario inventario;

	public delegate void AtualizarEquipamento();
    public AtualizarEquipamento atualizarEquipamento;

    public delegate void TrocaDeEquipamento(Equipamento itemNovo, Equipamento itemAntigo);
	public TrocaDeEquipamento trocaDeEquipamento;

	 void Start()
	 {
		inventario = Inventario.instance;

		int numSlots = System.Enum.GetNames(typeof(EquipamentoSlot)).Length;
		equipamentoAtual = new Equipamento[numSlots];
	 }

	public void equipar(Equipamento newItem)
	{
		int slotIndex = (int)newItem.getEquipamentoSlot();

		Equipamento itemAntigo = null;

		if (equipamentoAtual[slotIndex] != null)
		{
			itemAntigo = equipamentoAtual[slotIndex];
			inventario.adicionar(itemAntigo);
		}

		if (trocaDeEquipamento != null)
		{
			trocaDeEquipamento.Invoke(newItem, itemAntigo);
		}

		equipamentoAtual[slotIndex] = newItem;
        atualizarEquipamento.Invoke();
	}

	public void desequipar(int slotIndex) // mudar slotIndex para varivael "Selecionado"
	{
		if(equipamentoAtual[slotIndex] != null) 
		{
			Equipamento itemAntigo = equipamentoAtual[slotIndex];
			inventario.adicionar(itemAntigo);

			equipamentoAtual[slotIndex] = null;

			if (trocaDeEquipamento != null)
			{
				trocaDeEquipamento.Invoke(null, itemAntigo);
			}
            atualizarEquipamento.Invoke();
        }
	}

	public Equipamento[] getEquipamentoAtual() { return equipamentoAtual; }
}
