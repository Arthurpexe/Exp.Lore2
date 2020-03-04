using UnityEngine;

public class InstanciarBotao
{
    GameObject botaoInteragir;
    Transform posicaoBotao;
    GameObject player;
    float distancia;

    public InstanciarBotao()
    {
        botaoInteragir = GameObject.Find("BotaoInteragir");
        posicaoBotao = GameObject.Find("LugarBotaoInteragir").transform;
        player = GameObject.FindWithTag("Player");
    }

    public bool instanciarBotaoPorProximidade(Transform posObjetoGatilho, float raio)
    {
        distancia = (player.transform.position - posObjetoGatilho.position).magnitude;

        if (distancia <= raio)
        {
            if(posicaoBotao.childCount == 0)
                GameObject.Instantiate(botaoInteragir, posicaoBotao.transform.position, Quaternion.identity, posicaoBotao).name = "botao " + posObjetoGatilho.ToString();
            return true;
        }
        else
        {
            if (posicaoBotao.childCount > 0 && posicaoBotao.GetChild(0).name == "botao " + posObjetoGatilho.ToString())
                GameObject.Destroy(posicaoBotao.GetChild(0).gameObject);
            return false;
        }
    }
}
