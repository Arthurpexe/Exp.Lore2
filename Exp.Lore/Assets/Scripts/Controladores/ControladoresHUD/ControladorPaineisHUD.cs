using UnityEngine;

public class ControladorPaineisHUD : MonoBehaviour
{
    public GameObject painelInventario, marcadorNovoItem;
    public GameObject painelListaDeMissoes, marcadorNovaMissao;
    public GameObject painelAceitarQuest;
    public GameObject painelDialogo;
    public GameObject painelMenu;

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

            if (marcadorNovoItem.activeSelf)
                marcadorNovoItem.SetActive(false);
        }
        if (Input.GetButtonDown("Missoes"))
        {
            abrirPainel(painelListaDeMissoes);

            if (marcadorNovaMissao.activeSelf)
                marcadorNovaMissao.SetActive(false);
        }
        if (Input.GetButtonDown("Menu"))
        {
            if (painelInventario.activeSelf)
                painelInventario.SetActive(false);
            else if (painelListaDeMissoes.activeSelf)
                painelListaDeMissoes.SetActive(false);
            else if (painelAceitarQuest.activeSelf)
                painelAceitarQuest.SetActive(false);
            else if (painelDialogo.activeSelf)
                painelDialogo.SetActive(false);
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

    public void abrirPainel(GameObject painel)
    {
        painel.SetActive(!painel.activeSelf);

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
