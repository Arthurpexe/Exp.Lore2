using UnityEngine;
using UnityEngine.UI;

public class InventarioHUD : MonoBehaviour
{
    public Transform itensParent;

    public Text textoOuroJogador;

    Inventario inventario;

    InventarioSlot[] slotsItens;
    void Start()
    {
        inventario = Inventario.instance;
        inventario.onItemChangedCallback += atualizarInventarioHUD;
        slotsItens = itensParent.GetComponentsInChildren<InventarioSlot>();
    }

    public void atualizarInventarioHUD()
    {
        for (int i = 1; i < slotsItens.Length; i++)
        {
            if (i <= inventario.getListaItens().getContador())
            {
                if (inventario.getListaItens().localizarPorEndereco(i).meuItem != null)
                    slotsItens[i].adicionarItem(inventario.getListaItens().localizarPorEndereco(i).meuItem);
                else
                    slotsItens[i].limparSlot();
            }
            else
            {
                slotsItens[i].limparSlot();
            }
        }
    }

    public void atualizarOuroHUD(int ouro)
    {
        textoOuroJogador.text = ouro.ToString();
    }
}
