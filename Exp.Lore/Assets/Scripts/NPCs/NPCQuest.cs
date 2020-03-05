using UnityEngine;
using UnityEngine.UI;

public class NPCQuest : InteragirNPC
{
    [SerializeField]
    Missao missao;
    
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

        if ((bool)missao.info(Missao.TipoInformacao.concluida))
        {
            //conversação diferente caso já tenha completado a missão desse NPC
            textoDialogo.text = dialogoMissaoConcluida;
            painelDialogo.SetActive(!painelDialogo.activeSelf);
            return;
        }
        else if ((bool)missao.info(Missao.TipoInformacao.estaAtiva))
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

            textoTitulo.text = (string)missao.info(Missao.TipoInformacao.titulo);
            textoDescricao.text = (string)missao.info(Missao.TipoInformacao.descricao);
            textoOuro.text = ((int)missao.info(Missao.TipoInformacao.recompensaOuro)).ToString();
            if ((string)missao.info(Missao.TipoInformacao.titulo) == "A Invasão")
            {
                for (int i = 0; i < controladorPersonagem.missoes.Length; i++)
                {
                    if ((string)controladorPersonagem.missoes[i].info(Missao.TipoInformacao.titulo) == "Boatos (quase) Inacreditaveis")
                    {
                        controladorPersonagem.ouro += (int)controladorPersonagem.missoes[i].info(Missao.TipoInformacao.recompensaOuro);//controlador personagem n devia ta aqui
                        controladorPersonagem.missoes[i].concluirMissao();
                        controladorPersonagem.mudouMissao();
                    }
                }
            }
        }
        if (Input.GetButtonDown("Interagir"))
            painelAceitarQuest.SetActive(false);
    }

    public void aceitarQuest()
    {
        Debug.Log("Adicionei a quest " + (string)missao.info(Missao.TipoInformacao.titulo) + "!");
        missao.ativarMissao();
        Debug.Log(controladorPersonagem.gameObject.name);
        controladorPersonagem.missoes[controladorPersonagem.contadorMissoesAtivas] = missao;
        controladorPersonagem.contadorMissoesAtivas++;
        controladorPersonagem.mudouMissao();

        if ((string)missao.info(Missao.TipoInformacao.titulo) == "Começando Bem")
        {
            equipsTeste.SetActive(true);
        }

        if (((ObjetivoMissao) missao.info(Missao.TipoInformacao.objetivo)).meuObjetivo() == ObjetivoMissao.TipoObjetivo.falarCom)
        {
            //mudar a cor do NPCQuestMinimapa
        }
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
