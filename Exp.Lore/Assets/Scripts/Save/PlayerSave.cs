using UnityEngine;

[System.Serializable]
public class PlayerSave 
{
    ControladorPersonagem controladorPersonagem = ControladorPersonagem.instancia;

    public Vector3 posicao;
    public int vida;
    public Missao[] missoes;

    public PlayerSave()
    {
        posicao = new Vector3(0f, 0f, 0f);
        vida = 100;
        missoes = new Missao[6];
    }

    public void atualizarDependencias()
    {
        posicao = controladorPersonagem.transform.position;
        vida = controladorPersonagem.vidaAtual;
    }
    public void descarregarDependencias()
    {
        controladorPersonagem.transform.position = posicao;
        controladorPersonagem.vidaAtual = vida;
        controladorPersonagem.personagemStats.vidaAtual = vida;
        controladorPersonagem.personagemStats.carregarVida();
    }
}
