using UnityEngine;
using UnityEngine.UI;

public class InventarioHUD : MonoBehaviour
{
    public Transform itensParent;

    public GameObject painelInventario, marcadorNovoItem;
    public GameObject painelMissoes, marcadorNovaMissao;
    public GameObject painelAceitarQuest;
    public GameObject painelDialogo;
    public GameObject painelMenu;
    public Text textoOuroJogador;

    Inventario inventario;

    InventarioSlot[] slotsItens;
    void Start()
    {
        inventario = Inventario.instance;
        inventario.onItemChangedCallback += atualizarInventarioHUD;
        slotsItens = itensParent.GetComponentsInChildren<InventarioSlot>();

        ControladorPersonagem.instancia.seMissaoMudarCallback += marcadorNovaMissaoHUD;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventario"))
        {
            abrirPainel(painelInventario);

            if (marcadorNovoItem.activeSelf)
                marcadorNovoItem.SetActive(false);
        }
        if (Input.GetButtonDown("Missoes"))
        {
            abrirPainel(painelMissoes);

            if (marcadorNovaMissao.activeSelf)
                marcadorNovaMissao.SetActive(false);
        }
        if (Input.GetButtonDown("Menu"))
        {
            if (painelInventario.activeSelf)
                painelInventario.SetActive(false);
            else if (painelMissoes.activeSelf)
                painelMissoes.SetActive(false);
            else if (painelAceitarQuest.activeSelf)
                painelAceitarQuest.SetActive(false);
            else if (painelDialogo.activeSelf)
                painelDialogo.SetActive(false);
            else
                abrirPainel(painelMenu);
        }
    }

    public void atualizarInventarioHUD()
    {
        for (int i = 1; i < slotsItens.Length; i++)
        {
            if (i <= inventario.listaItens.contador)
            {
                if (inventario.listaItens.localizarPorEndereco(i).meuItem != null)
                    slotsItens[i].AddItem(inventario.listaItens.localizarPorEndereco(i).meuItem);
            }
            else
            {
                slotsItens[i].ClearSlot();
            }
        }

        textoOuroJogador.text = ControladorPersonagem.instancia.ouro.ToString();
        if (!painelInventario.activeSelf)
            marcadorNovoItem.SetActive(true);
    }

    public void marcadorNovaMissaoHUD()
    {
        if (!painelMissoes.activeSelf)
            marcadorNovaMissao.SetActive(true);
    }

    public void abrirPainel(GameObject painel)
    {
        painel.SetActive(!painel.activeSelf);

        if (marcadorNovoItem.activeSelf && painel == painelInventario)
        {
            marcadorNovoItem.SetActive(false);
        }
        if (marcadorNovaMissao.activeSelf && painel == painelMissoes)
        {
            marcadorNovaMissao.SetActive(false);
        }
    }
}
