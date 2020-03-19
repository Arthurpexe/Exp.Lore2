using UnityEngine;
using UnityEngine.UI;

public class VidaHUD : MonoBehaviour
{
    public Image barraVida;

    protected virtual void Start()
    {
        GetComponent<SerVivoStats>().seVidaMudar += seVidaMudar;
    }

    protected virtual void seVidaMudar(int vidaMaxima,int vidaAtual)
    {
        float porcentagemVida = vidaAtual / (float)vidaMaxima;
        barraVida.fillAmount = porcentagemVida;
    }
}
