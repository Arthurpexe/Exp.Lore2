using System.Collections;
using System.Collections.Generic;
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
            slotEquipCabeca.AddItem(controladorEquipamento.equipamentoAtual[0]);
            inventario.Remove(controladorEquipamento.equipamentoAtual[0]);

        }
        else
        {
            slotEquipCabeca.ClearSlot();
        }
        if(controladorEquipamento.equipamentoAtual[1] != null)
        {
            slotEquipPeito.AddItem(controladorEquipamento.equipamentoAtual[1]);
            inventario.Remove(controladorEquipamento.equipamentoAtual[1]);
        }
        else
        {
            slotEquipPeito.ClearSlot();
        }
        if (controladorEquipamento.equipamentoAtual[2] != null)
        {
            slotEquipPerna.AddItem(controladorEquipamento.equipamentoAtual[2]);
            inventario.Remove(controladorEquipamento.equipamentoAtual[2]);
        }
        else
        {
            slotEquipPerna.ClearSlot();
        }
        if (controladorEquipamento.equipamentoAtual[3] != null)
        {
            slotEquipPe.AddItem(controladorEquipamento.equipamentoAtual[3]);
            inventario.Remove(controladorEquipamento.equipamentoAtual[3]);
        }
        else
        {
            slotEquipPe.ClearSlot();
        }
    }
}
