using UnityEngine;

public class EquipamentoHUD : MonoBehaviour
{
    public InventarioSlot slotEquipCabeca, slotEquipPeito, slotEquipPerna, slotEquipPe;
    ControladorEquipamento controladorEquipamento;
    Inventario inventario;
    
    void Start()
    {
        inventario = Inventario.instance;
        controladorEquipamento = ControladorEquipamento.instance;
        controladorEquipamento.atualizarEquipamento += atualizarEquipsHUD;
    }

    public void atualizarEquipsHUD()
    {
        if(controladorEquipamento.equipamentoAtual[0] != null)
        {
            slotEquipCabeca.adicionarItem(controladorEquipamento.equipamentoAtual[0]);
            inventario.remover(controladorEquipamento.equipamentoAtual[0]);

        }
        else
        {
            slotEquipCabeca.limparSlot();
        }
        if(controladorEquipamento.equipamentoAtual[1] != null)
        {
            slotEquipPeito.adicionarItem(controladorEquipamento.equipamentoAtual[1]);
            inventario.remover(controladorEquipamento.equipamentoAtual[1]);
        }
        else
        {
            slotEquipPeito.limparSlot();
        }
        if (controladorEquipamento.equipamentoAtual[2] != null)
        {
            slotEquipPerna.adicionarItem(controladorEquipamento.equipamentoAtual[2]);
            inventario.remover(controladorEquipamento.equipamentoAtual[2]);
        }
        else
        {
            slotEquipPerna.limparSlot();
        }
        if (controladorEquipamento.equipamentoAtual[3] != null)
        {
            slotEquipPe.adicionarItem(controladorEquipamento.equipamentoAtual[3]);
            inventario.remover(controladorEquipamento.equipamentoAtual[3]);
        }
        else
        {
            slotEquipPe.limparSlot();
        }
    }
}
