using UnityEngine;
using UnityEngine.AI;
// Cuida das interações com o inimigo
[RequireComponent(typeof(PersonagemStats))]
public class Inimigo : MonoBehaviour
{
	Ref_posiçao_jogador playerManager;
	PersonagemStats myStats;

    public float radius = 3f;
    public Transform interactionTransform;
    public Transform player;
	

    private void Start()
	{
		playerManager = Ref_posiçao_jogador.instance;
		myStats = GetComponent<PersonagemStats>();
	}

    void Update()
    {
        PersonagemCombate playerCombat = playerManager.player.GetComponent<PersonagemCombate>();
        float distance = Vector3.Distance(player.position, interactionTransform.position);

		if (distance <= radius && Input.GetButtonDown("Atacar"))
        {
			
			
			if (playerCombat != null)
            {
                playerCombat.Ataque(myStats);
            }
        }
    }
}
