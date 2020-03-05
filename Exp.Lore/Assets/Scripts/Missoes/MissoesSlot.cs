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

        titulo.text = (string)missao.info(Missao.TipoInformacao.titulo);
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
        if((bool)missao.info(Missao.TipoInformacao.estaAtiva) || (bool)missao.info(Missao.TipoInformacao.concluida))
        {
            //descricao.text = missao.descricao;
            //recompensaOuro.text = missao.recompensaOuro.ToString();

            painelDescricao.SetActive(!painelDescricao.activeSelf);
        }
    }

    public void concluirMissao()
    {

        //if (slotMissaoConcluida != null)
        //{
            //if (slotMissaoConcluida.missao == null)
            //{
            //    slotMissaoConcluida.AdicionarMissao(this.missao);
            //}
            ClearSlot();
        //}
    }
}
