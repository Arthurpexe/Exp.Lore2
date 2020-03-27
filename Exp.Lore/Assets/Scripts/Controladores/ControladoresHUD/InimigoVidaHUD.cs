using UnityEngine;
using UnityEngine.UI;

public class InimigoVidaHUD : VidaHUD
{
    public GameObject vidaPrefab;
    public Transform posicaoBarraVida;
    public Canvas canvasWorldSpace;

    float tempoVisivel = 5;
    float ultimaVezVisto;
    Transform barraDeVida;

    Transform mainCamera;
    protected override void Start()
    {
        base.Start();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<Camera>().transform;

        barraDeVida = Instantiate(vidaPrefab, canvasWorldSpace.transform).transform;
        barraVida = barraDeVida.GetChild(0).GetComponent<Image>();
        barraDeVida.gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        if (barraDeVida != null)
        {
            barraDeVida.position = posicaoBarraVida.position;
            barraDeVida.forward = -mainCamera.forward;

            if (Time.time - ultimaVezVisto > tempoVisivel)
                barraDeVida.gameObject.SetActive(false);
        }
    }

    protected override void seVidaMudar(int vidaMaxima, int vidaAtual)
    {
        if (barraDeVida != null)
        {
            barraDeVida.gameObject.SetActive(true);
            ultimaVezVisto = Time.time;
            base.seVidaMudar(vidaMaxima, vidaAtual);
            if (vidaAtual <= 0)
                Destroy(barraDeVida.gameObject);
        }
    }
}
