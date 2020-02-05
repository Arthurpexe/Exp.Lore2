using UnityEngine;

public class PersonagemStats : MonoBehaviour
{
	Renderer rend;
    Color corInicial;
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
        Invoke("voltarCor", 0.075f);
        Debug.Log(transform.name + " Tomou " + dano + " dano.");

        seVidaMudar(vidaMaxima, vidaAtual);

		if(vidaAtual <= 0)
		{
            if (item != null)
            {
                Instantiate(item, this.transform.position, Quaternion.identity);
                item.GetComponent<Interagivel>().player = player;
            }
			MorrerAnimaçao();
		}

		
	}

    public void carregarVida()
    {
        seVidaMudar(vidaMaxima, vidaAtual);
        Debug.Log("vida atual "+vidaAtual);
    }


	public virtual void MorrerAnimaçao()
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
