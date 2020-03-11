using UnityEngine;
using UnityEngine.UI;

public class NPCQuest : InteragirNPC
{
    [SerializeField]
    Missao missao;
    ControladorMissoes controladorMissoes;

    public Text textoTitulo;
    public Text textoDescricao;
    public Text textoOuro;
    public Text textoNomeNPCMissao;
    public GameObject painelAceitarQuest;
    public GameObject botaoAceitarQuest;
    public GameObject falarComQuemMinimapa;
    public GameObject equipsTeste;

    [SerializeField]
    string dialogoMissaoAtiva;
    [SerializeField]
    string dialogoMissaoConcluida;

    protected override void Start()
    {
        base.Start();

        controladorMissoes = GameObject.Find("ControladorGeral").GetComponent<ControladorMissoes>();
    }

    protected override void Update()
    {
        base.Update();

        #region QuestEquipsTesteDESATIVADA
        //if (equipsTeste != null)
        //{
        //    if (!missao.concluida)
        //    {
        //        if (equipsTeste.transform.childCount == 0)
        //        {
        //            for (int i = 0; i < controladorPersonagem.missoes.Length; i++)
        //            {
        //                if (controladorPersonagem.missoes[i].titulo == "Começando Bem")
        //                {
        //                    controladorPersonagem.ouro += controladorPersonagem.missoes[i].recompensaOuro;//
        //                    controladorPersonagem.missoes[i].concluirMissao();
        //                    controladorPersonagem.mudouMissao();
        //                }
        //            }
        //        }
        //    }
        //}
        #endregion
    }

    public override void Interact()
    {
        base.Interact();

        if (missao.getConcluida())
        {
            //conversação diferente caso já tenha completado a missão desse NPC
            textoDialogo.text = dialogoMissaoConcluida;
            painelDialogo.SetActive(!painelDialogo.activeSelf);
            return;
        }
        else if (missao.getEstaAtiva())
        {
            //caso já esteja fazendo a missão desse NPC
            textoDialogo.text = dialogoMissaoAtiva;
            painelDialogo.SetActive(!painelDialogo.activeSelf);
            return;
        }

        if (!painelDialogo.activeSelf)
        {
            botaoAceitarQuest.GetComponent<Button>().onClick.RemoveAllListeners();
            botaoAceitarQuest.GetComponent<Button>().onClick.AddListener(respostaSim);

            painelAceitarQuest.SetActive(true);

            textoNomeNPCMissao.text = nomeNPC;

            textoTitulo.text = missao.getTitulo();
            textoDescricao.text = missao.getDescricao();
            textoOuro.text = missao.getRecompensaOuro().ToString();

            #region Missao "Boatos (quase) Inacreditaveis" Desativada
            //if (missao.getTitulo() == "A Invasão")
            //{
            //    for (int i = 0; i < controladorMissoes.missoes.Length; i++)
            //    {
            //        if (controladorPersonagem.missoes[i].getTitulo() == "Boatos (quase) Inacreditaveis")
            //        {
            //            controladorPersonagem.ouro += controladorPersonagem.missoes[i].getRecompensaOuro();//controlador personagem n devia ta aqui
            //            controladorPersonagem.missoes[i].concluirMissao();
            //            controladorPersonagem.mudouMissao();
            //        }
            //    }
            //}
            #endregion
        }
        else if(painelAceitarQuest.activeSelf)
        {
            painelDialogo.SetActive(false);
            painelAceitarQuest.SetActive(false);
        }
    }

    public void aceitarQuest()
    {
        Debug.Log(controladorMissoes.name);
        controladorMissoes.adicionarMissao(missao);
    }
    public void respostaSim()
    {
        aceitarQuest();
        painelAceitarQuest.SetActive(false);
    }
    public void respostaNao()
    {
        painelAceitarQuest.SetActive(false);
    }
}
