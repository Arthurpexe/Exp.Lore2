[System.Serializable]
public class ObjetivoMissao
{
    public TipoObjetivo tipoObjetivo;

    public int quantidadeNescessaria;
    public int quantidadeAtual;

    public ObjetivoMissao()
    {
        tipoObjetivo = TipoObjetivo.coletar;
        quantidadeAtual = 0;
        quantidadeNescessaria = 0;
    }
    public bool concluiu()
    {
        return (quantidadeAtual >= quantidadeNescessaria);
    }

    public void inimigoMorto()
    {
        if(tipoObjetivo == TipoObjetivo.matar)
            quantidadeAtual++;
    }

    public void coletarItem()
    {
        if (tipoObjetivo == TipoObjetivo.coletar)
            quantidadeAtual++;
    }

    public void chegouNumLugar()
    {
        if (tipoObjetivo == TipoObjetivo.irAte)
            quantidadeAtual++;
    }
}

public enum TipoObjetivo
{
    matar,
    coletar,
    irAte,
    falarCom
}
