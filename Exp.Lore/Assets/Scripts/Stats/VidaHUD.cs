using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SerVivoStats))]
public class VidaHUD : MonoBehaviour
{
    public Image barraVida;

    void Start()
    {
        GetComponent<SerVivoStats>().seVidaMudar += seVidaMudar;
    }

    public virtual void seVidaMudar(int vidaMaxima,int vidaAtual)
    {
        float porcentagemVida = vidaAtual / (float)vidaMaxima;
        barraVida.fillAmount = porcentagemVida;
    }
}
