using UnityEngine;

public class ControladorPersonagem : MonoBehaviour
{
    #region Singleton
    public static ControladorPersonagem instancia;

    private void Awake()
    {
        if (instancia != null)
        {
            Debug.LogWarning("Mais de uma instancia de ControladorPersonagem encontrada!");
            return;
        }
        instancia = this;

    }
	#endregion

	[Header("Paineis")]
    public GameObject painelMenu;
    public GameObject painelInventario;
    public GameObject painelFimDeJogo;

    [Header("Movimento")]
    public float SpeedIncrease = 10f;
	public float VelocidadeAbaixado = 0.5f;
    public float Speed = 5f;
    public float DashDistance = 2f;
    public int PlayerNumber = 1;
    public Rigidbody player;
    public Vector3 _inputs = Vector3.zero;
    public bool _isFastSpeed = false;
    private bool Abaixar = false;
    static Animator anim;
	public float CD = 3;
	private float cd;
    public ControladorCamera cameraPrincipal;

    [Header("Combate")]
    public PersonagemStats personagemStats;
    public int vidaAtual;
    public GameObject barraVidaBoss;
    public GameObject[] inimigos;
    public float distancia;
    public float distaciaMaxima = 5.0f;
	float Cooldown;
	private float cooldown;
    PersonagemCombate script;

    [Header("Save")]
    public PlayerSave personagem;

    [Header("Missoes")]
    public Missao[] missoes;
    public int contadorMissoesAtivas = 0;
    public int ouro;
    public GameObject titulo;


    public delegate void SeMissaoMudar();
    public SeMissaoMudar seMissaoMudarCallback;


    void Start()
	{
		missoes = new Missao[6];
		player = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
		script = GetComponent<PersonagemCombate>();
        personagem = new PlayerSave();
		Cooldown = script.CooldownAtaque;
		cooldown = Cooldown;

		personagemStats = this.GetComponent<PersonagemStats>();
		cd = CD;
    }

	void Update()
	{
		cooldown -= Time.deltaTime;
		cd -= Time.deltaTime;
		vidaAtual = personagemStats.vidaAtual;

        if (painelMenu.activeSelf || painelInventario.activeSelf || painelFimDeJogo.activeSelf)
        {
            _inputs = Vector3.zero;
            return;
        }

        

		_inputs = Vector3.zero;
		_inputs.x = Input.GetAxis("Horizontal");
		_inputs.z = Input.GetAxis("Vertical");
        if (_inputs != Vector3.zero)
        {
            anim.SetFloat("mov", 1);

            transform.forward = -_inputs;
        }

        if (Input.GetButtonDown("Correr_p1") && Abaixar == false) 
		{
			if (_isFastSpeed == false)
			{
				_isFastSpeed = true;
			}
		}

		if (_inputs.magnitude < 0.05f)
		{
			anim.SetFloat("mov", 0);
			_isFastSpeed = false;
		}

		if (_isFastSpeed == true)
		{
            anim.SetFloat("mov", 2);
            anim.SetBool("agachado", false);
			_inputs = _inputs * SpeedIncrease;
		}

		 if (_inputs != Vector3.zero && Abaixar == true)
		 {
			_inputs = _inputs * VelocidadeAbaixado;
		 }

		if (Input.GetButtonDown("Abaixar"))
		{
			if(Abaixar == false)
			{
				Abaixar = true;
                anim.SetBool("agachado", true);


			}


			else
			{
				Abaixar = false;
				anim.SetBool("agachado", false);
			}
		} 
		
			
        


		if (Abaixar == true && Input.GetButtonDown("Dash_p1") && cd <= 0)
		{
			anim.SetTrigger("rolar");
			_isFastSpeed = false;
			anim.SetBool("agachado", true);
			Vector3 dashVelocity = Vector3.Scale(transform.forward, DashDistance * new Vector3(5,0,5));
			player.AddForce(dashVelocity, ForceMode.VelocityChange);
			cd = CD;
		}

        if (Input.GetButtonDown("Atacar") && !_isFastSpeed && cooldown <= 0)
        {
            anim.SetTrigger("atacar");
			cooldown = Cooldown;
        }
    }

	void FixedUpdate()
	{
		player.MovePosition(player.position - _inputs * Speed * Time.fixedDeltaTime);
	}

    public void mudouMissao()
    {
        seMissaoMudarCallback.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "AreaBoss")
        {
            barraVidaBoss.SetActive(true);
            cameraPrincipal.dentroAreaBoss = true;
        }
        if (other.tag == "AreaBaseCachoeira")//verifico se cheguei na cachoeira para completar a missao Onde estão meus pais
        {
            for (int i = 0; i < missoes.Length; i++)
            {
                //Debug.Log("missão " + i + ": " + missoes[i].titulo);
                if (missoes[i].titulo == "Onde estão meus pais")
                {
                    if (missoes[i].estaAtiva)
                    {
                        missoes[i].objetivo.chegouNumLugar();
                        if (missoes[i].objetivo.concluiu())
                        {
                            ouro += missoes[i].recompensaOuro;
                            missoes[i].missaoConcluida();
                            //feedback de missao concluida
                            mudouMissao();
                        }

                    }
                }
            }
        }
        if (other.tag == "AreaMansãoRicasso")//verifico se cheguei na cachoeira para completar a missao Onde estão meus pais
        {
            for (int i = 0; i < missoes.Length; i++)
            {
                Debug.Log("missão " + i + ": " + missoes[i].titulo);
                if (missoes[i].estaAtiva)
                {
                    if (missoes[i].titulo == "A Invasão")
                    {
                        missoes[i].objetivo.chegouNumLugar();
                        if (missoes[i].objetivo.concluiu())
                        {
                            ouro += missoes[i].recompensaOuro;
                            missoes[i].missaoConcluida();
                            //feedback de missao concluida
                            mudouMissao();
                        }
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "AreaBoss")
        {
            barraVidaBoss.SetActive(false);
            cameraPrincipal.dentroAreaBoss = false;
        }
        if(other.tag == "AreaTitulo")
        {
            titulo.SetActive(false);
        }
    }
}
