using UnityEngine;
using System.IO;
using System.Xml.Serialization;
[System.Serializable]
public class SaveLoad
{
    Criptografia cripto;
    public PlayerSave playerSave;

    public SaveLoad(GameObject player)
    {
        cripto = new Criptografia();
        playerSave = new PlayerSave();
    }

    public void salvarPlayer()
    {
        playerSave.atualizarDependencias();
        //controladorPersonagem.personagem.missoes = controladorPersonagem.missoes;

        XmlSerializer serializador = new XmlSerializer(typeof(PlayerSave));
        StreamWriter arqDados = new StreamWriter("Player.xml");
        serializador.Serialize(arqDados.BaseStream, playerSave);
        arqDados.Close();

        cripto.criptografarArquivo("Player.xml", 'Ø');
        File.Delete("Player.xml");
        Debug.Log("salvei e criptografei o progresso do player");
    }

    public void carregarPlayer()
    {
        cripto.descriptografarArquivo("Player.xml", 'Ø');

        XmlSerializer serializador = new XmlSerializer(typeof(PlayerSave));
        StreamReader arqLeit = new StreamReader("Player.xml");
        playerSave = (PlayerSave)serializador.Deserialize(arqLeit.BaseStream);
        arqLeit.Close();
        Debug.Log("carreguei o player");

        playerSave.descarregarDependencias();
        //controladorPersonagem.personagem.missoes = aux.missoes;
        //controladorPersonagem.missoes = controladorPersonagem.personagem.missoes;
        //controladorPersonagem.mudouMissao();
        File.Delete("Player.xml");
        Time.timeScale = 1;
    }
}
