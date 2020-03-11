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
        svs = (SerVivoStats)controladorPersonagem.getInfo(ControladorPersonagem.Tipo.SerVivoStats);

        posicao = controladorPersonagem.transform.position;
        vida = svs.vidaAtual;
    }
    public void descarregarDependencias()
    {
        svs = (SerVivoStats)controladorPersonagem.getInfo(ControladorPersonagem.Tipo.SerVivoStats);

        controladorPersonagem.transform.position = posicao;
        svs.vidaAtual = vida;
        svs.carregarVida();
    }
}
