using UnityEngine;

public class SerVivoStats : MonoBehaviour
{
	protected Animator anim;
	Renderer rend;
	Color corInicial;
	[SerializeField]
	float tempoFeedbackDano = 0.1f;
	[SerializeField]
	protected int vidaMaxima = 100;
	int vidaAtual;
	[SerializeField]
	protected int dano = 3;
	[SerializeField]
	protected int armadura;

    public event System.Action<int, int> seVidaMudar;

	private void Awake()
	{
		rend = GetComponentInChildren<Renderer>();
        corInicial = rend.material.color;
		vidaAtual = vidaMaxima;
		anim = GetComponentInChildren<Animator>();
	}

	public void TomarDano(int dano)
	{
		dano -= armadura;
		dano = Mathf.Clamp(dano, 0, int.MaxValue);

		vidaAtual -= dano;
		if(vidaAtual < 0)
			vidaAtual = 0;

        rend.material.color = Color.red;
        Invoke("voltarCor", tempoFeedbackDano);
        Debug.Log(transform.name + " Tomou " + dano + " dano.");

		carregarVida();

		if(vidaAtual <= 0)
			MorrerAnimacao();
	}

    public int carregarVida()
    {
        seVidaMudar(vidaMaxima, vidaAtual);
        Debug.Log("vida atual "+vidaAtual);
		return vidaAtual;
    }

	public virtual void MorrerAnimacao(){}

	public virtual void Morrer(){}

    public void voltarCor()
    {
        rend.material.color = corInicial;
    }

	public void curar(int heal)
	{
		vidaAtual += heal;
		if(vidaAtual > vidaMaxima)
			vidaAtual = vidaMaxima;
		if (vidaAtual < 0)
			vidaAtual = 1;

		carregarVida();
	}
	public void aumentarArmadura(int armor)
	{
		armadura += armor;
	}
	public void aumentarDano(int damage)
	{
		dano += damage;
	}
	public void aumentarVidaMaxima(int maxHP)
	{
		vidaMaxima += maxHP;
	}

	public int getVidaAtual() { return vidaAtual; }
	public int getVidaMaxima() { return vidaMaxima; }
	public int getArmadura() { return armadura; }
	public int getDano() { return dano; }
}