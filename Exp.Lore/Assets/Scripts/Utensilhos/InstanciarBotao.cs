using UnityEngine;

public class InstanciarBotao
{
    GameObject botaoInteragir;
    Transform posicaoBotao;

    public InstanciarBotao()
    {
        botaoInteragir = GameObject.Find("BotaoInteragir");
        posicaoBotao = GameObject.Find("LugarBotaoInteragir").transform;
    }

    public bool instanciarBotaoPorProximidade(Vector3 posObjetoGatilho, float raio, GameObject objetoTrigger)
    {
        float distancia = (objetoTrigger.transform.position - posObjetoGatilho).magnitude;

        if (distancia <= raio)
        {
            if(posicaoBotao.childCount == 0)
                GameObject.Instantiate(botaoInteragir, posicaoBotao.transform.position, Quaternion.identity, posicaoBotao).name = "botao " + posObjetoGatilho;
            return true;
        }
        else
        {
            if (posicaoBotao.childCount > 0 && posicaoBotao.GetChild(0).name == "botao " + posObjetoGatilho)
                GameObject.Destroy(posicaoBotao.GetChild(0).gameObject);
            return false;
        }
    }
}
