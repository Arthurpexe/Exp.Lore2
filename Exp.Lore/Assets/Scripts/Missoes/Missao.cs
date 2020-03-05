using UnityEngine;
[System.Serializable]
public class Missao
{
    ControladorPersonagem controladorPersonagem;

    bool estaAtiva;
    bool concluida;
    bool jaMostrouNaHUD;

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
        if(controladorPersonagem != null)
            controladorPersonagem = ControladorPersonagem.instancia;
        estaAtiva = true;
        controladorPersonagem.mudouMissao();
    }
    public void concluirMissao()
    {
        if (controladorPersonagem != null)
            controladorPersonagem = ControladorPersonagem.instancia;//TEMPORARIO
        concluida = true;
        estaAtiva = false;
        controladorPersonagem.ouro += recompensaOuro;//TEMPORARIO
        controladorPersonagem.mudouMissao();//TEMPORARIO
    }
    public void mostreiNaHUD()
    {
        jaMostrouNaHUD = true;
    }

    public void invadiuRicassius()
    {
        if(titulo == "A Invasão" && !concluida)
        {
            objetivo.chegouNumLugar();
            concluirMissao();
        }
    }

    public object info(TipoInformacao ti)
    {
        switch (ti)
        {
            case TipoInformacao.estaAtiva:
                return estaAtiva;
            case TipoInformacao.concluida:
                return concluida;
            case TipoInformacao.jaMostrouNaHUD:
                return jaMostrouNaHUD;
            case TipoInformacao.titulo:
                return titulo;
            case TipoInformacao.descricao:
                return descricao;
            case TipoInformacao.recompensaOuro:
                return recompensaOuro;
            case TipoInformacao.objetivo:
                return objetivo;
            default:
                return null;
        }
    }

    public enum TipoInformacao { estaAtiva, concluida, jaMostrouNaHUD, titulo, descricao, recompensaOuro, objetivo}
}
