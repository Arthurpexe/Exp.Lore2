using UnityEngine;
using UnityEngine.UI;

public class MissoesHUD : MonoBehaviour
{
    ControladorMissoes controladorMissoes;

    public GameObject painelDescricaoMissao;
    public GameObject painelMissaoConcluida;
    public Transform espacoMissoesAtivas;
    public Transform espacoMissoesConcluidas;
    public MissaoSlot[] slotsMissoesAtivas;
    public MissaoSlot[] slotsMissoesConcluidas;

    void Start()
    {
        controladorMissoes = GameObject.Find("ControladorGeral").GetComponent<ControladorMissoes>();
        controladorMissoes.seMissaoMudarCallback += atualizarMissoesHUD;

        criarSlotsMissoes();
    }

    void criarSlotsMissoes()
    {
        slotsMissoesAtivas = new MissaoSlot[6];
        slotsMissoesConcluidas = new MissaoSlot[6];
        for (int i = 0; i < 6; i++)
        {
            slotsMissoesAtivas[i] = new MissaoSlot(espacoMissoesAtivas.GetChild(i), painelDescricaoMissao);
        }
    }

    public void mostrarDescricaoMissaoAtiva(int slot)
    {
        slotsMissoesAtivas[slot].MostrarDescricao();
    }
    public void mostrarDescricaoMissaoConcluida(int slot)
    {
        slotsMissoesConcluidas[slot].MostrarDescricao();
    }

    public void atualizarMissoesHUD()
    {
        for (int i = 0; i < controladorMissoes.getMissoes().Length; i++)
        {
            Missao[] missoesAux = controladorMissoes.getMissoes();
            if (i <= controladorMissoes.getContadorMissoesAtivas())
            {
                if (missoesAux[i] != null)
                {
                    if (missoesAux[i].getEstaAtiva())
                    {
                        slotsMissoesAtivas[i].AdicionarMissao(missoesAux[i]);
                    }
                    else if (missoesAux[i].getConcluida())
                    {
                        slotsMissoesConcluidas[i].AdicionarMissao(missoesAux[i]);
                        slotsMissoesAtivas[i].ClearSlot();

                        if (missoesAux[i + 1] != null)
                        {
                            if (missoesAux[i + 1].getEstaAtiva())
                            {
                                slotsMissoesAtivas[i].AdicionarMissao(missoesAux[i + 1]);
                                slotsMissoesAtivas[i + 1].ClearSlot();
                                controladorMissoes.diminuirIndiceMissoes(i);
                            }
                        }
                        if (!missoesAux[i].getJaMostrouNaHUD())
                        {
                            painelMissaoConcluida.GetComponentInChildren<Text>().text = "Missão " + missoesAux[i].getTitulo() + " concluida!";
                            painelMissaoConcluida.SetActive(true);
                            Invoke("desativarPainelMissaoConcluida", 5f);
                            missoesAux[i].mostreiNaHUD();
                        }
                    }
                }
            }
            else
            {
                slotsMissoesAtivas[i].ClearSlot();
            }
        }
        Debug.Log("Mudei a HUD das Missoes");
    }

    public void desativarPainelMissaoConcluida()
    {
        painelMissaoConcluida.SetActive(false);
    }
}
