using UnityEngine;

public class EquipamentoHUD : MonoBehaviour
{
    public GameObject gridEspacosEquips;
    InventarioSlot slotEquipCabeca, slotEquipPeito, slotEquipPerna, slotEquipPe;
    Inventario inventario;
    
    void Start()
    {
        inventario = Inventario.instance;
        ControladorEquipamento.instance.atualizarEquipamento += atualizarEquipsHUD;

        slotEquipCabeca = gridEspacosEquips.GetComponentsInChildren<InventarioSlot>()[0];
        slotEquipPeito = gridEspacosEquips.GetComponentsInChildren<InventarioSlot>()[1];
        slotEquipPerna = gridEspacosEquips.GetComponentsInChildren<InventarioSlot>()[2];
        slotEquipPe = gridEspacosEquips.GetComponentsInChildren<InventarioSlot>()[3];
    }

    public void atualizarEquipsHUD(Equipamento[] equipamentoAtualHUD)
    {
        if (equipamentoAtualHUD[0] != null)
        {
            slotEquipCabeca.adicionarItem(equipamentoAtualHUD[0]);
            inventario.remover(equipamentoAtualHUD[0]);
        }
        else
        {
            slotEquipCabeca.limparSlot();
        }
        if(equipamentoAtualHUD[1] != null)
        {
            slotEquipPeito.adicionarItem(equipamentoAtualHUD[1]);
            inventario.remover(equipamentoAtualHUD[1]);
        }
        else
        {
            slotEquipPeito.limparSlot();
        }
        if (equipamentoAtualHUD[2] != null)
        {
            slotEquipPerna.adicionarItem(equipamentoAtualHUD[2]);
            inventario.remover(equipamentoAtualHUD[2]);
        }
        else
        {
            slotEquipPerna.limparSlot();
        }
        if (equipamentoAtualHUD[3] != null)
        {
            slotEquipPe.adicionarItem(equipamentoAtualHUD[3]);
            inventario.remover(equipamentoAtualHUD[3]);
        }
        else
        {
            slotEquipPe.limparSlot();
        }
    }
}
