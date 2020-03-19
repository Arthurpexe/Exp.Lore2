using UnityEngine;

[CreateAssetMenu(fileName = "Novo Equipamento", menuName = "Inventário/Equipamento")]
public class Equipamento : Item
{
    ControladorEquipamento controladorEquipamento;

    [SerializeField]
	EquipamentoSlot equipamentoSlot;
    [SerializeField]
	int armaduraModificador;
    [SerializeField]
    int danoModificador;
    [SerializeField]
    int vidaMaximaModificador;

    public override void Use()
	{
        base.Use();

        controladorEquipamento = ControladorEquipamento.instance;
		controladorEquipamento.Equipar(this);
        //colocar na aba equipamentos no slot referente ao enum EquipamentoSlot
        RemoverDoInventario();
    }

    public override void desequipar()
    {
        base.desequipar();
        controladorEquipamento.Desequipar((int)equipamentoSlot);
    }
    public EquipamentoSlot getEquipamentoSlot() { return equipamentoSlot; }
    public int getArmaduraModificador() { return armaduraModificador; }
    public int getDanoModificador() { return danoModificador; }
    public int getVidaMaximaModificador() { return vidaMaximaModificador; }
}

public enum EquipamentoSlot { Capacete, Peitoral, Pernas, Pés}
