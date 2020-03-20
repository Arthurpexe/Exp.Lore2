using UnityEngine;
using UnityEngine.UI;

public class MissaoSlot : MonoBehaviour
{
    public GameObject painelDescricao;

    public Text titulo;
    Text tituloDescricao;
    Text descricao;
    Text recompensaOuro;

    Missao missao;

    private void Start()
    {
        tituloDescricao = painelDescricao.transform.GetChild(0).GetComponent<Text>();
        descricao = painelDescricao.transform.GetChild(1).GetComponent<Text>();
        recompensaOuro = painelDescricao.transform.GetChild(4).GetComponent<Text>();
    }
    public void AdicionarMissao(Missao novaMissao)
    {
        missao = novaMissao;

        titulo.text = missao.getTitulo();
        titulo.enabled = true;
    }

    public void ClearSlot()
    {
        missao = null;

        titulo.text = null;
        titulo.enabled = false;
    }

    public void MostrarDescricao()
    {
        if (missao.getEstaAtiva() || missao.getConcluida())
        {
            tituloDescricao.text = missao.getTitulo();
            descricao.text = missao.getDescricao();
            recompensaOuro.text = missao.getRecompensaOuro().ToString();

            painelDescricao.SetActive(!painelDescricao.activeSelf);
        }
    }
}