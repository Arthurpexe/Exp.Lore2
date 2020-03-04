using UnityEngine;
using UnityEngine.UI;

public class InteragirNPCQuest : Interagivel
{
    [SerializeField]
    Missao missao;
    [SerializeField]
    string nomeNPC;
    public Text textoNomeNPCDialogo;
    public Text textoTitulo;
    public Text textoDescricao;
    public Text textoOuro;
    public Text textoNomeNPCMissao;
    public GameObject painelAceitarQuest;
    public GameObject botaoAceitarQuest;
    public GameObject falarComQuemMinimapa;
    public GameObject equipsTeste;

    public GameObject painelDialogo;
    [SerializeField]
    string dialogo;
    [SerializeField]
    string dialogoMissaoAtiva;
    Text textoDialogo;
    InstanciarBotao instanciarBotao;

    private void Start()
    {
        controladorPersonagem = ControladorPersonagem.instancia;
        textoDialogo = painelDialogo.GetComponentInChildren<Text>();
        instanciarBotao = new InstanciarBotao();
    }

    private void Update()
    {
        if (instanciarBotao.instanciarBotaoPorProximidade(transform, raio))
        {
            if (Input.GetButtonDown("Interagir"))
            {
                Interact();
            }
        }


        
        if (equipsTeste != null)
        {
            if (!this.missao.concluida)
            {
                if (equipsTeste.transform.childCount == 0)
                {
                    for (int i = 0; i < controladorPersonagem.missoes.Length; i++)
                    {
                        if (controladorPersonagem.missoes[i].titulo == "Começando Bem")
                        {
                            controladorPersonagem.ouro += controladorPersonagem.missoes[i].recompensaOuro;//
                            controladorPersonagem.missoes[i].concluirMissao();
                            controladorPersonagem.mudouMissao();
                        }
                    }
                }
            }
        }
    }

    public void Interact()
    {
        base.Interact();

        textoNomeNPCDialogo.text = nomeNPC;

        if (this.missao.concluida)
        {
            //conversação diferente
            textoDialogo.text = dialogo;
            painelDialogo.SetActive(!painelDialogo.activeSelf);
            return;
        }
        else if (this.missao.estaAtiva)
        {
            textoDialogo.text = dialogoMissaoAtiva;
            painelDialogo.SetActive(!painelDialogo.activeSelf);
            return;
        }
        

        //conversação

        botaoAceitarQuest.GetComponent<Button>().onClick.RemoveAllListeners();
        botaoAceitarQuest.GetComponent<Button>().onClick.AddListener(this.respostaSim);

        painelAceitarQuest.SetActive(true);

        textoNomeNPCMissao.text = nomeNPC;

        if (!missao.concluida)
        {
            textoTitulo.text = missao.titulo;
            textoDescricao.text = missao.descricao;//
            textoOuro.text = missao.recompensaOuro.ToString();//

            if (missao.titulo == "A Invasão")
            {
                for (int i = 0; i < controladorPersonagem.missoes.Length; i++)
                {

                    if (controladorPersonagem.missoes[i].titulo == "Boatos (quase) Inacreditaveis")
                    {
                        controladorPersonagem.ouro += controladorPersonagem.missoes[i].recompensaOuro;//
                        controladorPersonagem.missoes[i].concluirMissao();
                        controladorPersonagem.mudouMissao();
                    }
                }
            }
        }
    }

    public void aceitarQuest()
    {
        Debug.Log("Adicionei a quest "+missao.titulo+"!");
        this.missao.estaAtiva = true;
        Debug.Log(controladorPersonagem.gameObject.name);
        controladorPersonagem.missoes[controladorPersonagem.contadorMissoesAtivas] = this.missao;
        controladorPersonagem.contadorMissoesAtivas++;
        controladorPersonagem.mudouMissao();

        if (missao.titulo == "Começando Bem")
        {
            equipsTeste.SetActive(true);
        }

        if (missao.objetivo.tipoObjetivo == TipoObjetivo.falarCom)
        {
            //mudar a cor do NPCQuestMinimapa
        }
    }
    public void respostaSim()
    {
        this.aceitarQuest();
        painelAceitarQuest.SetActive(false);
    }
    public void respostaNao()
    {
        painelAceitarQuest.SetActive(false);
    }
}
