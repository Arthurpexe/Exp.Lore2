using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PersonagemStats))]
public class VidaHUD : MonoBehaviour
{
    public Image barraVida;

    void Start()
    {
        GetComponent<PersonagemStats>().seVidaMudar += seVidaMudar;
    }

    public virtual void seVidaMudar(int vidaMaxima,int vidaAtual)
    {
        float porcentagemVida = vidaAtual / (float)vidaMaxima;
        barraVida.fillAmount = porcentagemVida;
    }
}
