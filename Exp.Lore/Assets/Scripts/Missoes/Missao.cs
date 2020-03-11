using UnityEngine;
[System.Serializable]
public class Missao
{
    ControladorMissoes controladorMissoes;

    bool estaAtiva = false;
    bool concluida = false;
    bool jaMostrouNaHUD = false;

    [SerializeField]
    string titulo;
    [SerializeField]
    string descricao;
    [SerializeField]
    int recompensaOuro;

    [SerializeField]
    ObjetivoMissao objetivo;

    public Missao()
    {
        estaAtiva = false;
        concluida = false;
        jaMostrouNaHUD = false;
        titulo = null;
        descricao = null;
        recompensaOuro = 0;
        objetivo = new ObjetivoMissao();
    }
    public void ativarMissao()
    {
        if(controladorMissoes != null)
            controladorMissoes = GameObject.Find("ControladorGeral").GetComponent<ControladorMissoes>();
        estaAtiva = true;
        controladorMissoes.atualizarMissoesCallback();
    }
    public int concluirMissao()
    {
        if (controladorMissoes != null)
            controladorMissoes = GameObject.Find("ControladorGeral").GetComponent<ControladorMissoes>();
        concluida = true;
        estaAtiva = false;
        controladorMissoes.atualizarMissoesCallback();
        return recompensaOuro;
    }
    public void mostreiNaHUD()
    {
        jaMostrouNaHUD = true;
    }

    public bool getEstaAtiva() { return estaAtiva; }
    public bool getConcluida() { return concluida; }
    public bool getJaMostrouNaHUD() { return jaMostrouNaHUD; }
    public string getTitulo() { return titulo; }
    public string getDescricao() { return descricao; }
    public int getRecompensaOuro() { return recompensaOuro; }
    public ObjetivoMissao getObjetivo() { return objetivo; }
}
