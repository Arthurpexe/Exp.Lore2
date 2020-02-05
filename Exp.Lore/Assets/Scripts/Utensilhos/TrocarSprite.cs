using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrocarSprite : MonoBehaviour
{
    public GameObject objetoAtivador;

    [Header("Alterar Sprite")]
    public Image imagemIcone;
    public Sprite spriteNormal, spriteAlterado;

    [Header("Alterar Texto")]
    public Text textoObjetivo;
    public string fraseAAlterar;


    private string textoOriginal;

    private void Start()
    {
        if(textoObjetivo != null)
        {
            textoOriginal = textoObjetivo.text;
        }
    }
    private void Update()
    {
        if(imagemIcone != null)
        {
            trocarSprite();
        }
        if(textoObjetivo != null)
        {
            trocarTexto();
        }
    }

    public void trocarSprite()
    {
        if (objetoAtivador.activeSelf)
            imagemIcone.sprite = spriteAlterado;
        else
            imagemIcone.sprite = spriteNormal;
    }
    public void trocarTexto()
    {
        if (objetoAtivador.activeSelf)
            textoObjetivo.text = fraseAAlterar;
        else
            textoObjetivo.text = textoOriginal;
    }
}
