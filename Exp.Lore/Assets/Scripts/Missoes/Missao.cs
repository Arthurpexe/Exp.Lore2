[System.Serializable]
public class Missao
{
    ControladorPersonagem controladorPersonagem;

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
        titulo = null;
        descricao = null;
        recompensaOuro = 0;
        objetivo = new ObjetivoMissao();
    }
    public void concluirMissao()
    {
        controladorPersonagem = ControladorPersonagem.instancia;
        concluida = true;
        estaAtiva = false;
        controladorPersonagem.ouro += recompensaOuro;
        controladorPersonagem.mudouMissao();
    }

    public void invadiuRicassius()
    {
        if(titulo == "A Invasão" && !concluida)
        {
            objetivo.chegouNumLugar();
            concluirMissao();
        }
    }
}
