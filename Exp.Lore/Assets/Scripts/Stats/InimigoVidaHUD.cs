using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InimigoVidaHUD : VidaHUD
{
    public GameObject vidaPrefab;
    public Transform posicaoBarraVida;
    public Canvas c;

    float tempoVisivel = 5;
    float ultimaVezVisto;
    Transform ui;

    Transform mainCamera;
    void Start()
    {
        mainCamera = Camera.main.transform;

        ui = Instantiate(vidaPrefab, c.transform).transform;
        barraVida = ui.GetChild(0).GetComponent<Image>();
        ui.gameObject.SetActive(false);

        GetComponent<PersonagemStats>().seVidaMudar += seVidaMudar;
    }

    void LateUpdate()
    {
        if (ui != null)
        {
            ui.position = posicaoBarraVida.position;
            ui.forward = -mainCamera.forward;

            if (Time.time - ultimaVezVisto > tempoVisivel)
                ui.gameObject.SetActive(false);
        }
    }

    public override void seVidaMudar(int vidaMaxima, int vidaAtual)
    {
        if (ui != null)
        {
            ui.gameObject.SetActive(true);
            ultimaVezVisto = Time.time;
            base.seVidaMudar(vidaMaxima, vidaAtual);
            if (vidaAtual <= 0)
                Destroy(ui.gameObject);
        }
    }
}
