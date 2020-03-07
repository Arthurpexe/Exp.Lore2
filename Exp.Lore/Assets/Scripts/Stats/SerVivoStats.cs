using UnityEngine;

public class SerVivoStats : MonoBehaviour
{
	Renderer rend;
    Color corInicial;
	[SerializeField]
	float tempoFeedbackDano = 0.1f;
	public int vidaMaxima = 100;
    public int vidaAtual; //{ get; private set; }
	public int danoInimigo;
	public int dano;
	public int armadura;
	

	public GameObject item;
	public GameObject player;

    public event System.Action<int, int> seVidaMudar;

	private void Awake()
	{
		rend = GetComponentInChildren<Renderer>();
        corInicial = rend.material.color;
		vidaAtual = vidaMaxima;
        player = GameObject.FindWithTag("Player");
    }


	public void TomarDano(int dano)
	{
		dano -= armadura;
		dano = Mathf.Clamp(dano, 0, int.MaxValue);

		vidaAtual -= dano;
        rend.material.color = Color.red;
        Invoke("voltarCor", tempoFeedbackDano);
        Debug.Log(transform.name + " Tomou " + dano + " dano.");

        seVidaMudar(vidaMaxima, vidaAtual);

		if(vidaAtual <= 0)
		{
            if (item != null)
            {
                Instantiate(item, this.transform.position + Vector3.up, Quaternion.identity);
            }
			MorrerAnimacao();
		}

		
	}

    public void carregarVida()
    {
        seVidaMudar(vidaMaxima, vidaAtual);
        Debug.Log("vida atual "+vidaAtual);
    }


	public virtual void MorrerAnimacao()
	{
	}

	public virtual void Morrer()
	{
	}
    public void voltarCor()
    {
        rend.material.color = corInicial;
    }
}
