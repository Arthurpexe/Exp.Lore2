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
    PlayerSave playerSave;

    //só para não bugar a animação
    public GameObject personagem;
	Animator anim;

    private void Start()
    {
        cripto = new Criptografia();
        playerSave = new PlayerSave();
		anim = personagem.GetComponentInChildren<Animator>();
    }

    public void salvarPlayer()
    {
        playerSave.atualizarDependencias();
        //controladorPersonagem.personagem.missoes = controladorPersonagem.missoes;

        XmlSerializer serializador = new XmlSerializer(typeof(PlayerSave));
        StreamWriter arqDados = new StreamWriter("Player.xml");
        serializador.Serialize(arqDados.BaseStream, playerSave);
        arqDados.Close();

        cripto.criptografarArquivo("Player.xml", '§');
        Debug.Log("salvei e criptografei o player");
    }

    public void carregarPlayer()
    {
        cripto.descriptografarArquivo("Player.xml", '§');

        XmlSerializer serializador = new XmlSerializer(typeof(PlayerSave));
        StreamReader arqLeit = new StreamReader("Player.xml");
        playerSave = (PlayerSave)serializador.Deserialize(arqLeit.BaseStream);
        arqLeit.Close();
        Debug.Log("carreguei o player");

        playerSave.descarregarDependencias();
        //controladorPersonagem.personagem.missoes = aux.missoes;
        //controladorPersonagem.missoes = controladorPersonagem.personagem.missoes;
        //controladorPersonagem.mudouMissao();

        Time.timeScale = 1;
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
