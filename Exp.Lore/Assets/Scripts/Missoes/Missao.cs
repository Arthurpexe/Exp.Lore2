using UnityEngine;
[System.Serializable]
public class Missao
{
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
        estaAtiva = true;
    }
    public int concluirMissao()
    {
        concluida = true;
        estaAtiva = false;
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
