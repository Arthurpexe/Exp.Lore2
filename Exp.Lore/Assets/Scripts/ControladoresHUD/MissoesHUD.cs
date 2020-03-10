using UnityEngine;
using UnityEngine.UI;

public class MissoesHUD : MonoBehaviour
{
    ControladorPersonagem controladorPersonagem;

    public GameObject painelMissaoConcluida;
    public MissoesSlot[] slotsMissoesAtivas = new MissoesSlot[6];
    public MissoesSlot[] slotsMissoesConcluidas = new MissoesSlot[6];
    void Start()
    {
        controladorPersonagem = ControladorPersonagem.instancia;
        controladorPersonagem.seMissaoMudarCallback += atualizarMissoesHUD;
    }

    public void atualizarMissoesHUD()
    {
        for (int i = 0; i < controladorPersonagem.missoes.Length; i++)
        {
            if (i <= controladorPersonagem.contadorMissoesAtivas)
            {
                if (controladorPersonagem.missoes[i] != null)
                {
                    if ((bool)controladorPersonagem.missoes[i].info(Missao.TipoInformacao.estaAtiva))
                    {
                        slotsMissoesAtivas[i].AdicionarMissao(controladorPersonagem.missoes[i]);
                    }
                    else if ((bool)controladorPersonagem.missoes[i].info(Missao.TipoInformacao.concluida))
                    {
                        slotsMissoesConcluidas[i].AdicionarMissao(controladorPersonagem.missoes[i]);
                        slotsMissoesAtivas[i].concluirMissao();

                        if (!(bool)controladorPersonagem.missoes[i].info(Missao.TipoInformacao.jaMostrouNaHUD))
                        {
                            painelMissaoConcluida.GetComponentInChildren<Text>().text = "Missão " + (string)controladorPersonagem.missoes[i].info(Missao.TipoInformacao.titulo) + " concluida!";
                            painelMissaoConcluida.SetActive(true);
                            Invoke("desativarPainelMissaoConcluida", 5f);
                            controladorPersonagem.missoes[i].mostreiNaHUD();
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
