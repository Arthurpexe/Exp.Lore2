using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    #region Singleton
    public static SaveLoad instancia;

    private void Awake()
    {
        if(instancia != null)
        {
            Debug.LogWarning("Mais de uma instancia de SaveLoad encontrada!");
            return;
        }
        instancia = this;

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    Criptografia cripto;

    public GameObject personagem;

    ControladorPersonagem controladorPersonagem;

	Animator anim;

    private void Start()
    {
        cripto = new Criptografia();
        controladorPersonagem = ControladorPersonagem.instancia;
		anim = personagem.GetComponentInChildren<Animator>();
    }

    public void CarregarJogo()
    {
        Time.timeScale = 1;

        carregarPlayer();
    }

    public void salvarPlayer()
    {
        controladorPersonagem.personagem.posicao = controladorPersonagem.player.transform.position;
        controladorPersonagem.personagem.vida = controladorPersonagem.vidaAtual;
        //controladorPersonagem.personagem.missoes = controladorPersonagem.missoes;

        XmlSerializer serializador = new XmlSerializer(typeof(PlayerSave));
        StreamWriter arqDados = new StreamWriter("Player.xml");
        serializador.Serialize(arqDados.BaseStream, controladorPersonagem.personagem);
        arqDados.Close();

        cripto.criptografarArquivo("Player.xml", '§');
        Debug.Log("salvei e criptografei o player");
    }

    public void carregarPlayer()
    {
        cripto.descriptografarArquivo("Player.xml", '§');

        XmlSerializer serializador = new XmlSerializer(typeof(PlayerSave));
        StreamReader arqLeit = new StreamReader("Player.xml");
        PlayerSave aux = (PlayerSave)serializador.Deserialize(arqLeit.BaseStream);
        arqLeit.Close();
        Debug.Log("carreguei o player");


        controladorPersonagem.personagem.posicao = aux.posicao;
        controladorPersonagem.player.transform.position = controladorPersonagem.personagem.posicao;

        controladorPersonagem.personagem.vida = aux.vida;
        controladorPersonagem.personagemStats.vidaAtual = controladorPersonagem.personagem.vida;
        controladorPersonagem.personagemStats.carregarVida();

        //controladorPersonagem.personagem.missoes = aux.missoes;
        //controladorPersonagem.missoes = controladorPersonagem.personagem.missoes;
        //controladorPersonagem.mudouMissao();

        anim.Rebind();
    }

    public void sairJogo()
    {
        Application.Quit();
    }

    public void Add(System.Object ot)
    {
        throw new FileNotFoundException();
    }
}
