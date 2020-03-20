using UnityEngine;
using UnityEngine.UI;

public class MissoesHUD : MonoBehaviour
{
    ControladorMissoes controladorMissoes;

    public GameObject painelMissaoAtiva, marcadorNovaMissao;
    public GameObject painelMissaoConcluida;
    public Transform espacosMissoesAtivas;
    public Transform espacosMissoesConcluidas;
    MissaoSlot[] slotsMissoesAtivas = new MissaoSlot[6];
    MissaoSlot[] slotsMissoesConcluidas = new MissaoSlot[6];

    void Start()
    {
        controladorMissoes = GameObject.Find("ControladorGeral").GetComponent<ControladorMissoes>();
        controladorMissoes.seMissaoMudarCallback += atualizarMissoesHUD;
        controladorMissoes.seMissaoMudarCallback += marcadorNovaMissaoHUD;
        associarSlotsMissoes(0);
    }

    void associarSlotsMissoes(int cont)
    {
        if (cont < 6)
        {
            slotsMissoesAtivas[cont] = espacosMissoesAtivas.GetChild(cont).GetComponent<MissaoSlot>();
            slotsMissoesConcluidas[cont] = espacosMissoesConcluidas.GetChild(cont).GetComponent<MissaoSlot>();
            cont++;
            associarSlotsMissoes(cont);
        }
    }

    public void atualizarMissoesHUD()
    {
        Missao[] missoesAux = controladorMissoes.getMissoes();

        for (int i = 0; i < controladorMissoes.getMissoes().Length; i++)
        {
            if (i <= controladorMissoes.getContadorMissoesAtivas())
            {
                if (missoesAux[i] != null)
                {
                    if (missoesAux[i].getEstaAtiva())
                    {
                        Debug.Log(slotsMissoesAtivas[i].name);
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

    public void marcadorNovaMissaoHUD()
    {
        if (!painelMissaoAtiva.activeSelf)
            marcadorNovaMissao.SetActive(true);
    }
    

    public void desativarPainelMissaoConcluida()
    {
        painelMissaoConcluida.SetActive(false);
    }
}
