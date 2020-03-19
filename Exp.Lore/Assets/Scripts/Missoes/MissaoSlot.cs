using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MissaoSlot
{
    GameObject painelDescricaoMissao;

    Text tituloSlot;
    Text tituloDescricao;
    Text descricao;
    Text recompensaOuro;

    [SerializeField]
    Missao missao;

    public MissaoSlot(Transform meuSlot, GameObject painelDescricao)
    {
        painelDescricaoMissao = painelDescricao;
        tituloDescricao = painelDescricaoMissao.transform.GetChild(0).GetComponent<Text>();
        descricao = painelDescricaoMissao.transform.GetChild(1).GetComponent<Text>();
        recompensaOuro = painelDescricaoMissao.transform.GetChild(4).GetComponent<Text>();

        tituloSlot = meuSlot.GetChild(0).GetChild(0).GetComponent<Text>();
    }
    public void AdicionarMissao(Missao novaMissao)
    {
        missao = novaMissao;

        tituloSlot.text = missao.getTitulo();
        tituloSlot.enabled = true;
    }

    public void ClearSlot()
    {
        missao = null;

        tituloSlot.text = null;
        tituloSlot.enabled = false;
    }

    public void MostrarDescricao()
    {
        if (missao.getEstaAtiva() || missao.getConcluida())
        {
            tituloDescricao.text = missao.getTitulo();
            descricao.text = missao.getDescricao();
            recompensaOuro.text = missao.getRecompensaOuro().ToString();

            painelDescricaoMissao.SetActive(!painelDescricaoMissao.activeSelf);
        }
    }
}
