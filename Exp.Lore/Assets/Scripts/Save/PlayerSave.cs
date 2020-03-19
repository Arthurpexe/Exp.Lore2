using UnityEngine;

[System.Serializable]
public class PlayerSave 
{
    ControladorPersonagem controladorPersonagem;
    SerVivoStats svs;
    public Vector3 posicao;
    public int vida;
    public Missao[] missoes;

    public PlayerSave()
    {
        controladorPersonagem = ControladorPersonagem.instancia;
        posicao = new Vector3(0f, 0f, 0f);
        vida = 100;
        missoes = new Missao[6];
    }

    public void atualizarDependencias()
    {
        svs = controladorPersonagem.getSerVivoStats();

        posicao = controladorPersonagem.transform.position;
        vida = svs.getVidaAtual();
    }
    public void descarregarDependencias()
    {
        svs = controladorPersonagem.getSerVivoStats();

        controladorPersonagem.transform.position = posicao;
        svs.curar(svs.getVidaMaxima());
        svs.curar(vida - svs.getVidaMaxima());
    }
}
