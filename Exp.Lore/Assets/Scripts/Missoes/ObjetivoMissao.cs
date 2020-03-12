using UnityEngine;
[System.Serializable]
public class ObjetivoMissao
{
    [SerializeField]
    TipoObjetivo tipoObjetivo;

    [SerializeField]
    int quantidadeNescessaria;
    int quantidadeAtual;

    public ObjetivoMissao()
    {
        tipoObjetivo = TipoObjetivo.coletar;
        quantidadeAtual = 0;
        quantidadeNescessaria = 0;
    }
    private bool concluiu()
    {
        return (quantidadeAtual >= quantidadeNescessaria);
    }

    public bool inimigoMorto()
    {
        if(tipoObjetivo == TipoObjetivo.matar)
            quantidadeAtual++;
        return concluiu();
    }

    public bool coletarItem()
    {
        if (tipoObjetivo == TipoObjetivo.coletar)
            quantidadeAtual++;
        return concluiu();
    }

    public bool chegouNumLugar()
    {
        if (tipoObjetivo == TipoObjetivo.irAte)
            quantidadeAtual++;
        return concluiu();
    }

    public TipoObjetivo meuObjetivo()
    {
        return tipoObjetivo;
    }

    public enum TipoObjetivo { matar, coletar, irAte, falarCom}
}
