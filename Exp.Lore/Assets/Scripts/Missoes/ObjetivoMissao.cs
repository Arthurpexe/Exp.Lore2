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

    public TipoObjetivo meuObjetivo()
    {
        return tipoObjetivo;
    }

    public enum TipoObjetivo { matar, coletar, irAte, falarCom}
}
