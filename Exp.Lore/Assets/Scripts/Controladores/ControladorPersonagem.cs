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

    GameObject controladorGeral;
    ControladorJogo controladorJogo;
    InventarioHUD inventarioHUD;

    //movimentação do personagem
    MovimentoPersonagem movimentoPersonagem;
    Vector3 inputs = Vector3.zero;
	float cdRolamentoMax = 3;
	float cdRolamentoAtual;
    bool pausa;

    //parte de combate
    SerVivoStats serVivoStats;

    //parte das missoes
    ControladorMissoes controladorMissoes;
    [SerializeField]
    int ouro;

    void Start()
	{
        controladorGeral = GameObject.Find("ControladorGeral");
        controladorMissoes = controladorGeral.GetComponent<ControladorMissoes>();
        controladorJogo = controladorGeral.GetComponent<ControladorJogo>();
        inventarioHUD = GameObject.Find("HUDCanvas").GetComponent<InventarioHUD>();
        movimentoPersonagem = new MovimentoPersonagem(GetComponent<Rigidbody>(), GetComponentInChildren<Animator>());

		serVivoStats = GetComponent<SerVivoStats>();
		cdRolamentoAtual = cdRolamentoMax;
    }

	void Update()
	{
        inputs = Vector3.zero;

        if (pausa)//se o jogo estiver pausado o jogador não vai poder se mexer
            return;

        //Area dos cooldowns
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
        if (movimentoPersonagem.getCorrendo())
		{
			inputs = inputs * movimentoPersonagem.getVelocidadeCorrendo();//pegando a velocidade correndo do movimento do personagem
        }

        //se o personagem estiver abaixado e estiver andando
        if (inputs != Vector3.zero && movimentoPersonagem.getAbaixado())
		{
		    inputs = inputs * movimentoPersonagem.getVelocidadeAbaixado();//pegando a velocidade abaixado do movimento do personagem
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

    public void atualizarOuro(int ouroGanho)
    {
        ouro += ouroGanho;
        inventarioHUD.atualizarOuroHUD(ouro);
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
            controladorMissoes.entreiAreaMansao();
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

    public SerVivoStats getSerVivoStats(){ return serVivoStats; }
    public int getOuro() { return ouro; }
}
