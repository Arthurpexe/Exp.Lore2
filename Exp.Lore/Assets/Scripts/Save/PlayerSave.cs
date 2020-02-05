using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave 
{
    public Vector3 posicao;
    public int vida;
    public Missao[] missoes;

    public PlayerSave()
    {
        this.posicao = new Vector3(0f, 0f, 0f);
        this.vida = 100;
        this.missoes = new Missao[6];
    }
}
