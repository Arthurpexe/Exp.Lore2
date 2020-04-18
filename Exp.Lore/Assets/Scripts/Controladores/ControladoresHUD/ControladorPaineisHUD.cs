using UnityEngine;

public class ControladorPaineisHUD : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject painelInventario, marcadorNovoItem;
    public GameObject painelListaDeMissoes, marcadorNovaMissao;
    public GameObject painelAceitarQuest;
    public GameObject painelDialogo;
    public GameObject painelMenu;
    public GameObject painelFimDeJogo;

    void Start()
    {
        GameObject cg = GameObject.Find("ControladorGeral");
        cg.GetComponent<ControladorMissoes>().seMissaoMudarCallback += marcadorNovaMissaoHUD;
        cg.GetComponent<Inventario>().onItemChangedCallback += marcadorNovoItemHUD;
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventario"))
        {
            abrirPainel(painelInventario);
        }
        if (Input.GetButtonDown("Missoes"))
        {
            abrirPainel(painelListaDeMissoes);
        }
        if (Input.GetButtonDown("Menu"))
        {
            //se tiver algum painel ativo o botão de menu vai fecha-lo primeiro
            if (painelInventario.activeSelf)
                abrirPainel(painelInventario);
            else if (painelListaDeMissoes.activeSelf)
                abrirPainel(painelListaDeMissoes);
            else if (painelAceitarQuest.activeSelf)
                abrirPainel(painelAceitarQuest);
            else if (painelDialogo.activeSelf)
                abrirPainel(painelDialogo);
            else
                abrirPainel(painelMenu);
        }
    }

    void marcadorNovoItemHUD()
    {
        if (!painelInventario.activeSelf)
            marcadorNovoItem.SetActive(true);
    }

    void marcadorNovaMissaoHUD()
    {
        if (!painelListaDeMissoes.activeSelf)
            marcadorNovaMissao.SetActive(true);
    }

    /// <summary>
    /// se o painel estiver desativado ele ativa, se estiver ativado desativa.
    /// </summary>
    /// <param name="painel">painel que vai ser ativado/desativado</param>
    public void abrirPainel(GameObject painel)
    {
        painel.SetActive(!painel.activeSelf);

        //configurações do cursor
        if (painel.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        //marcadores de novos itens de interesse
        if (marcadorNovoItem.activeSelf && painel == painelInventario)
        {
            marcadorNovoItem.SetActive(false);
        }
        if (marcadorNovaMissao.activeSelf && painel == painelListaDeMissoes)
        {
            marcadorNovaMissao.SetActive(false);
        }
    }
}
