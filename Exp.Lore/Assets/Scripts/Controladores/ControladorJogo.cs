using UnityEngine;

public class ControladorJogo : MonoBehaviour
{
    SaveLoad saveLoad;
    ControladorPersonagem controladorPersonagem;
    ControladorCamera cameraPrincipal;

    [Header("Paineis")]
    public GameObject painelMenu;
    public GameObject painelInventario;
    public GameObject painelFimDeJogo;
    public GameObject barraDeVidaBoss;

    void Start()
    {
        saveLoad = new SaveLoad(GameObject.FindWithTag("Player"));
        controladorPersonagem = ControladorPersonagem.instancia;
        cameraPrincipal = GameObject.Find("Main Camera").GetComponent<ControladorCamera>();
    }
    private void Update()
    {
        //se algum desses paineis estiver ativo o jogo vai ser pausado
        if (painelMenu.activeSelf || painelInventario.activeSelf || painelFimDeJogo.activeSelf)
            pausaJogo();
        else
            despausaJogo();
    }

    public void salvarJogo(){ saveLoad.salvarPlayer(); }

    public void carregarJogo(){ saveLoad.carregarPlayer(); }

    public void sairJogo(){ Application.Quit(); }

    public void pausaJogo()
    {
        Time.timeScale = 0.1f;
        controladorPersonagem.pausarPlayer(true);
    }
    public void despausaJogo()
    {
        Time.timeScale = 1;
        controladorPersonagem.pausarPlayer(false);
    }

    public void entreiAreaBoss()
    {
        cameraPrincipal.fixaBoss();
        barraDeVidaBoss.SetActive(true);
    }
    public void saiAreaBoss()
    {
        cameraPrincipal.seguePlayer();
        barraDeVidaBoss.SetActive(false);
    }
}
