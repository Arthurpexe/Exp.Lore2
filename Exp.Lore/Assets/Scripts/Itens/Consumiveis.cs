using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Novo COnsumivel", menuName = "Inventário/Consumivel")]

public class Consumiveis : Item
{
    public int vida;
    public int dano;
    public int armadura;
    GameObject player;
    JogadorStats stats;

    public override void Use()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<JogadorStats>();

        
        
            if (stats.vidaAtual + vida <= 100)
            {

                stats.vidaAtual = stats.vidaAtual + vida;

        }
        else
        {
            stats.vidaAtual = 100;
        }

            if (stats.dano + dano <= 100)
            {
                stats.dano = stats.dano + dano;
        }
        else
        {
            stats.dano = 100;
        }

            if (stats.armadura + armadura <= 100)
            {
                stats.armadura = stats.armadura + armadura;
        }
        else
        {
            stats.armadura = 100;
        }
        

        RemoverDoInventario();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
