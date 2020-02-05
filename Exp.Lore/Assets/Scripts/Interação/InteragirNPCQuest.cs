using UnityEngine;
using UnityEngine.UI;

public class InteragirNPCQuest : Interagivel
{
    public Missao missao;

    public string nomeNPC;
    public Text textoNomeNPCDialogo;
    public bool aceita = false;
    public GameObject painelAceitarQuest;
    public Text textoTitulo;
    public Text textoDescricao;
    public Text textoOuro;
    public Text textoNomeNPCMissao;
    public GameObject botaoAceitarQuest;
    public GameObject falarComQuemMinimapa;
    public GameObject equipsTeste;

    public GameObject painelDialogo;
    public string dialogo;
    public string dialogoMissaoAtiva;
    Text textoDialogo;

    public GameObject botaoInteragir;
    public Transform paiBotaoInteragir;

    private void Start()
    {
        controladorPersonagem = ControladorPersonagem.instancia;
        textoDialogo = painelDialogo.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, interactionTransform.position);
        if (distance <= radius)
        {
            if (paiBotaoInteragir.childCount == 0)
            {
                Instantiate(botaoInteragir, paiBotaoInteragir);
            }
            if (Input.GetButtonDown("Interagir"))
            {
                Interact();
                Destroy(paiBotaoInteragir.GetChild(0).gameObject);
            }
        }
        else
        {
            if (paiBotaoInteragir.childCount > 0)
            {
                Destroy(paiBotaoInteragir.GetChild(0).gameObject);
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
                            controladorPersonagem.ouro += controladorPersonagem.missoes[i].recompensaOuro;
                            controladorPersonagem.missoes[i].missaoConcluida();
                            controladorPersonagem.mudouMissao();
                        }
                    }
                }
            }
        }
    }

    public override void Interact()
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
            this.textoTitulo.text = this.missao.titulo;
            this.textoDescricao.text = this.missao.descricao;
            this.textoOuro.text = this.missao.recompensaOuro.ToString();

            if (missao.titulo == "A Invasão")
            {
                for (int i = 0; i < controladorPersonagem.missoes.Length; i++)
                {

                    if (controladorPersonagem.missoes[i].titulo == "Boatos (quase) Inacreditaveis")
                    {
                        controladorPersonagem.ouro += controladorPersonagem.missoes[i].recompensaOuro;
                        controladorPersonagem.missoes[i].missaoConcluida();
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
