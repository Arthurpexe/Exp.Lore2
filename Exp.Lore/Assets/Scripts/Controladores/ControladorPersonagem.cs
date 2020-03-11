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
    Vector3 inputs = Vector3.zero;
	float cdRolamentoMax = 3;
	float cdRolamentoAtual;
    bool pausa;

    //parte de combate
    SerVivoStats personagemStats;

    [Header("Missoes")]
    public Missao[] missoes;//criar um controlador de missoes
    public int contadorMissoesAtivas = 0;
    public int ouro;


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

		personagemStats = GetComponent<SerVivoStats>();
		cdRolamentoAtual = cdRolamentoMax;
    }

	void Update()
	{
        inputs = Vector3.zero;

        if (pausa)//se o jogo estiver pausado o jogador não vai poder se mexer
            return;

        //Area dos cooldowns
        //cooldown -= Time.deltaTime;
		cdRolamentoAtual -= Time.deltaTime;

        //Area de inputs
        inputs.x = Input.GetAxis("Horizontal");
		inputs.z = Input.GetAxis("Vertical");

        if (inputs != Vector3.zero)
        {
            movimentoPersonagem.andar(inputs);
        }

        if (Input.GetButtonDown("Correr_p1")) 
		{
            movimentoPersonagem.correr();
		}

		if (inputs.magnitude < 0.05f)
		{
            movimentoPersonagem.parar();
		}

        //se o personagem estiver correndo
        if ((bool)movimentoPersonagem.info(MovimentoPersonagem.TipoInformacao.correndo))
		{
			inputs = inputs * (float)movimentoPersonagem.info(MovimentoPersonagem.TipoInformacao.velocidadeCorrendo);//pegando a velocidade correndo do movimento do personagem
        }

        //se o personagem estiver abaixado e estiver andando
        if (inputs != Vector3.zero && (bool)movimentoPersonagem.info(MovimentoPersonagem.TipoInformacao.abaixado))
		{
		    inputs = inputs * (float)movimentoPersonagem.info(MovimentoPersonagem.TipoInformacao.velocidadeAbaixado);//pegando a velocidade abaixado do movimento do personagem
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
    }

	void FixedUpdate()
	{
        movimentoPersonagem.movePosicao(inputs);
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
        if (other.tag == "AreaMansãoRicasso")//verifico se cheguei na mansão do ricassius para completar a missao de chegar lá
        {
            for (int i = 0; i < missoes.Length; i++)
            {
                Debug.Log("missão " + i + ": " + (string)missoes[i].info(Missao.TipoInformacao.titulo));
                missoes[i].invadiuRicassius();
            }
        }
        if (other.tag == "AreaTitulo")
        {
            StartCoroutine(controladorJogo.entreiAreaTitulo());
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
            StartCoroutine(controladorJogo.saiAreaTitulo());
        }
    }

    public object getInfo(Tipo t)
    {
        switch (t)
        {
            case Tipo.SerVivoStats:
                return personagemStats;
            default:
                return null;
        }
    }
    public enum Tipo { SerVivoStats }
}
