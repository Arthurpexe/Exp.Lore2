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
    SerVivoStats meusStats;
    ControladorMissoes controladorMissoes;
    [SerializeField]
    int ouro;

    //movimentação do personagem
    [SerializeField]
    MovimentoPersonagem movimentoPersonagem;
    Vector3 inputs = Vector3.zero;
	float cdRolamentoMax = 3;
	float cdRolamentoAtual;
    bool pausa;

    void Start()
	{
        controladorGeral = GameObject.Find("ControladorGeral");
        controladorMissoes = controladorGeral.GetComponent<ControladorMissoes>();
        controladorJogo = controladorGeral.GetComponent<ControladorJogo>();
        inventarioHUD = GameObject.Find("HUDCanvas").GetComponent<InventarioHUD>();
        movimentoPersonagem = new MovimentoPersonagem(GetComponent<Rigidbody>(), GetComponentInChildren<Animator>());

		meusStats = GetComponent<SerVivoStats>();
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

		if (Input.GetButtonDown("Abaixar"))
		{
            movimentoPersonagem.abaixar();
		} 

		if (Input.GetButtonDown("Dash_p1") && cdRolamentoAtual <= 0)
		{
            movimentoPersonagem.rolar();
			cdRolamentoAtual = cdRolamentoMax;
		}

        if (Input.GetKeyDown(KeyCode.U))//HACK pra curar 10 de vida instantaneamente
        {
            meusStats.curar(10);
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

    public SerVivoStats getSerVivoStats(){ return meusStats; }
    public int getOuro() { return ouro; }
}
