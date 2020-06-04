using System;
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

    //ataque
    GameObject inimigosObj;
    SerVivoStats[] inimigos;
    float rangeAtaque = 5;
    float cdAtaqueMax = 1;
    float cdAtaqueAtual;

    void Start()
	{
        controladorGeral = GameObject.Find("ControladorGeral");
        controladorMissoes = controladorGeral.GetComponent<ControladorMissoes>();
        controladorJogo = controladorGeral.GetComponent<ControladorJogo>();
        inventarioHUD = GameObject.Find("HUDCanvas").GetComponent<InventarioHUD>();
        movimentoPersonagem = new MovimentoPersonagem(GetComponent<Rigidbody>(), GetComponentInChildren<Animator>());

        inimigosObj = GameObject.Find("Inimigos");
        

		meusStats = GetComponent<SerVivoStats>();
		cdRolamentoAtual = cdRolamentoMax;
        cdAtaqueAtual = cdAtaqueMax;
    }

	void Update()
	{
        inputs = Vector3.zero;

        if (pausa)//se o jogo estiver pausado o jogador não vai poder se mexer
            return;

        //Area dos cooldowns
		cdRolamentoAtual -= Time.deltaTime;
        cdAtaqueAtual -= Time.deltaTime;

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

        if (Input.GetButtonDown("Atacar") && cdAtaqueAtual <= 0)
        {
            if (tentarAtacar())
                cdAtaqueAtual = cdAtaqueMax;
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

    bool tentarAtacar()
    {
        inimigos = inimigosObj.GetComponentsInChildren<SerVivoStats>();
        foreach (SerVivoStats inimigo in inimigos)
        {
            float distancia = Vector3.Distance(transform.position, inimigo.transform.position);
            if(distancia <= rangeAtaque)
            {
                meusStats.atacar(inimigo.GetComponentInChildren<SerVivoStats>());
                return true;
            }
            else if (inimigo.CompareTag("Boss") && distancia <= rangeAtaque * 2)//gambiarra
            {
                meusStats.atacar(inimigo.GetComponentInChildren<SerVivoStats>());
                return true;
            }
        }
        Debug.Log("nenhum inimigo dentro do range de ataque");
        return false;
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeAtaque);

        Gizmos.color = Color.yellow;//gambiarra
        Gizmos.DrawWireSphere(transform.position, rangeAtaque * 2);//
    }
    public SerVivoStats getSerVivoStats(){ return meusStats; }
    public int getOuro() { return ouro; }
}
