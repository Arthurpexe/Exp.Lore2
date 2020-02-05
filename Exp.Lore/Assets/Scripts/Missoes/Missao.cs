[System.Serializable]
public class Missao
{
    public bool estaAtiva;
    public bool concluida;
    public bool jaMostrouNaHUD;

    public string titulo;
    public string descricao;
    public int recompensaOuro;

    public ObjetivoMissao objetivo;

    public Missao()
    {
        estaAtiva = false;
        concluida = false;
        jaMostrouNaHUD = false;
        titulo = "novaMissao";
        descricao = "novaDescricao";
        recompensaOuro = 0;
        objetivo = new ObjetivoMissao();
    }
    public void missaoConcluida()
    {
        concluida = true;
        estaAtiva = false;
    }
}
