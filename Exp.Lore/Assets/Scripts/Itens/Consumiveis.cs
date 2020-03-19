using UnityEngine;

[CreateAssetMenu(fileName = "Novo COnsumivel", menuName = "Inventário/Consumivel")]
public class Consumiveis : Item
{
    [SerializeField]
    int vida;
    [SerializeField]
    int armadura;
    GameObject player;
    JogadorStats stats;

    public override void Use()//TEM Q MUDAR ISSO AQUI chamar alguma função do jogadorStats
    {
        base.Use();

        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<JogadorStats>();

        stats.curar(vida);

        stats.aumentarArmadura(armadura);
        
        RemoverDoInventario();
    }
}
