using System.Collections;
using UnityEngine;

public class ControladorJogo : MonoBehaviour
{
    SaveLoad saveLoad;
    ControladorPersonagem controladorPersonagem;
    ControladorCamera cameraPrincipal;

    [Header("GameObjects")]
    public GameObject painelMenu;
    public GameObject painelInventario;
    public GameObject painelFimDeJogo;
    public GameObject barraDeVidaBoss;
    public GameObject titulo;

    void Start()
    {
        saveLoad = new SaveLoad(GameObject.FindWithTag("Player"));
        controladorPersonagem = ControladorPersonagem.instancia;
        cameraPrincipal = GameObject.Find("Main Camera").GetComponent<ControladorCamera>();
    }
    void Update()
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

    public IEnumerator entreiAreaTitulo()
    {
        Transform[] filhosTitulo = new Transform[titulo.transform.childCount];
        for (int i = 0; i < titulo.transform.childCount; i++)
        {
            filhosTitulo[i] = titulo.transform.GetChild(i);
        }

        float j = 0.1f;
        while (j < 1)
        {
            yield return new WaitForSeconds(0.03f);
            titulo.transform.GetChild(0).localScale = new Vector3(j, j, j);
            foreach (var transform in filhosTitulo)
            {
                if (transform.GetSiblingIndex() != 0)
                {
                    transform.gameObject.SetActive(true);
                }
            }
            j += 0.1f;
        }
    }
    public IEnumerator saiAreaTitulo()
    {
        Transform[] filhosTitulo = new Transform[titulo.transform.childCount];
        for(int i = 0; i < titulo.transform.childCount; i++)
        {
            filhosTitulo[i] = titulo.transform.GetChild(i);
        }

        float j = 1;
        while (j > 0.1f)
        {
            yield return new WaitForSeconds(0.03f);
            titulo.transform.GetChild(0).localScale = new Vector3(j, j, j);
            foreach(var transform in filhosTitulo)
            {
                if(transform.GetSiblingIndex() != 0)
                {
                    transform.gameObject.SetActive(false);
                }
            }
            j -= 0.1f;
        }
    }
}
