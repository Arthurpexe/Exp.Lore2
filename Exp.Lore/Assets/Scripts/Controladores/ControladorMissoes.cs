using UnityEngine;

public class ControladorMissoes : MonoBehaviour
{
    ControladorPersonagem controladorPersonagem;

    Missao[] missoes;
    int contadorMissoesAtivas = 0;

    public delegate void SeMissaoMudar();
    public SeMissaoMudar seMissaoMudarCallback;
    void Start()
    {
        controladorPersonagem = ControladorPersonagem.instancia;
        missoes = new Missao[6];
    }

    public void diminuirIndiceMissoes(int i)
    {
        missoes[i] = missoes[i + 1];
        missoes[i + 1] = null;
    }

    public void adicionarMissao(Missao missao)
    {
        missao.ativarMissao();
        missoes[contadorMissoesAtivas] = missao;
        contadorMissoesAtivas++;
        atualizarMissoesCallback();

        if (missao.getObjetivo().meuObjetivo() == ObjetivoMissao.TipoObjetivo.falarCom)
        {
            //mudar a cor do NPCQuestMinimapa
        }
    }

    public void atualizarMissoesCallback()
    {
        seMissaoMudarCallback.Invoke();
    }

    public void entreiAreaMansao()
    {
        for (int i = 0; i < missoes.Length; i++)
        {
            if(missoes[i].getTitulo() == "A Invasão" && !missoes[i].getConcluida())
            {
                if (missoes[i].getObjetivo().chegouNumLugar())
                {
                    controladorPersonagem.atualizarOuro(missoes[i].concluirMissao());
                    atualizarMissoesCallback();
                    //atualizar posição das missões na HUD
                    //contadorMissoesAtivas--;
                }
            }
        }
    }

    public void matarBoss()
    {
        for (int i = 0; i < missoes.Length; i++)
        {
            if(missoes[i].getTitulo() == "A Hora da Verdade")
            {
                controladorPersonagem.atualizarOuro(missoes[i].concluirMissao());
                atualizarMissoesCallback();
            }
        }
    }

    public Missao[] getMissoes() { return missoes; }
    public int getContadorMissoesAtivas() { return contadorMissoesAtivas; }
}
