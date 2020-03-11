using UnityEngine;
using UnityEngine.UI;

public class MissoesSlot : MonoBehaviour
{
    public GameObject painelDescricao;

    public Text titulo;
    public Text descricao;
    public Text recompensaOuro;

    public Missao missao;

    //public MissoesSlot slotMissaoConcluida;
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
        if(missao.getEstaAtiva() || missao.getConcluida())
        {
            //descricao.text = missao.descricao;
            //recompensaOuro.text = missao.recompensaOuro.ToString();

            painelDescricao.SetActive(!painelDescricao.activeSelf);
        }
    }
}
