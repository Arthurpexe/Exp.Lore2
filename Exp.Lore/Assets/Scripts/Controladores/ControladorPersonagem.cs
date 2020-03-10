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

    ControladorJogo controladorJogo;

    //movimentação do personagem
    MovimentoPersonagem movimentoPersonagem;
    Vector3 _inputs = Vector3.zero;
	float cdRolamentoMax = 3;
	float cdRolamentoAtual;
    bool pausa;

    [Header("Combate")]
    public SerVivoStats personagemStats;
    public int vidaAtual;
    public GameObject[] inimigos;
    public float distancia;
    public float distaciaMaxima = 5.0f;
	//float Cooldown;
	//private float cooldown;
    //PersonagemCombate script;

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
        movimentoPersonagem = new MovimentoPersonagem(GetComponent<Rigidbody>(), GetComponentInChildren<Animator>());
		//script = GetComponent<PersonagemCombate>();
        controladorJogo = GameObject.Find("ControladorGeral").GetComponent<ControladorJogo>();
		//Cooldown = script.CooldownAtaque;
		//cooldown = Cooldown;

		personagemStats = this.GetComponent<SerVivoStats>();
		cdRolamentoAtual = cdRolamentoMax;
    }

	void Update()
	{
        _inputs = Vector3.zero;

        if (pausa)//se o jogo estiver pausado o jogador não vai poder se mexer
            return;

        //Area dos cooldowns
        //cooldown -= Time.deltaTime;
		cdRolamentoAtual -= Time.deltaTime;
		vidaAtual = personagemStats.vidaAtual;//acho q tem q mudar isso

        //Area de inputs
        _inputs.x = Input.GetAxis("Horizontal");
		_inputs.z = Input.GetAxis("Vertical");

        if (_inputs != Vector3.zero)
        {
            movimentoPersonagem.andar(_inputs);
        }

        if (Input.GetButtonDown("Correr_p1")) 
		{
            movimentoPersonagem.correr();
		}

		if (_inputs.magnitude < 0.05f)
		{
            movimentoPersonagem.parar();
		}

        //se o personagem estiver correndo
        if ((bool)movimentoPersonagem.info(MovimentoPersonagem.TipoInformacao.correndo))
		{
			_inputs = _inputs * (float)movimentoPersonagem.info(MovimentoPersonagem.TipoInformacao.velocidadeCorrendo);//pegando a velocidade correndo do movimento do personagem
        }

        //se o personagem estiver abaixado e estiver andando
        if (_inputs != Vector3.zero && (bool)movimentoPersonagem.info(MovimentoPersonagem.TipoInformacao.abaixado))
		{
		    _inputs = _inputs * (float)movimentoPersonagem.info(MovimentoPersonagem.TipoInformacao.velocidadeAbaixado);//pegando a velocidade abaixado do movimento do personagem
        }

		if (Input.GetButtonDown("Abaixar"))
		{
            movimentoPersonagem.abaixar();
		} 

		if (Input.GetButtonDown("Dash_p1") && cdRolamentoAtual <= 0)
		{
            movimentoPersonagem.rolar();
			cdRolamentoAtual = cdRolamentoMax;
		}

        //já que não vai ter mais combate não precisa disso
   //     if (Input.GetButtonDown("Atacar") && !_isFastSpeed && cooldown <= 0)
   //     {
   //         anim.SetTrigger("atacar");
   //		  cooldown = Cooldown;
   //     }
    }

	void FixedUpdate()
	{
        movimentoPersonagem.movePosicao(_inputs);
	}

    public void mudouMissao()
    {
        seMissaoMudarCallback.Invoke();
    }

    public void pausarPlayer(bool pausado)
    {
        pausa = pausado;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "AreaBoss")
        {
            controladorJogo.entreiAreaBoss();
        }

        if (other.tag == "AreaMansãoRicasso")//verifico se cheguei na cachoeira para completar a missao Onde estão meus pais
        {
            for (int i = 0; i < missoes.Length; i++)
            {
                Debug.Log("missão " + i + ": " + (string)missoes[i].info(Missao.TipoInformacao.titulo));
                missoes[i].invadiuRicassius();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "AreaBoss")
        {
            controladorJogo.saiAreaBoss();
        }
        if(other.tag == "AreaTitulo")
        {
            titulo.SetActive(false);
        }
    }
}
