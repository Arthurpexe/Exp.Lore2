using UnityEngine;

public class BossStats : InimigoStats
{
    public AudioSource audioListenerBoss;
    ControladorMissoes controladorMissoes;

    void Start()
    {
        controladorMissoes = GameObject.Find("ControladorGeral").GetComponent<ControladorMissoes>();
    }

    public override void MorrerAnimacao()
    {
        base.MorrerAnimacao();
        audioListenerBoss.enabled = false;
        controladorMissoes.matarBoss();
    }
}
