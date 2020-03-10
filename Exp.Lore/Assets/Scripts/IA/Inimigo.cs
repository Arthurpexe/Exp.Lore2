using UnityEngine;
using UnityEngine.AI;
// Cuida das interações com o inimigo
[RequireComponent(typeof(SerVivoStats))]
public class Inimigo : MonoBehaviour
{
	SerVivoStats myStats;

    public float radius = 3f;
    public Transform interactionTransform;
    public Transform player;
	

    private void Start()
	{
		myStats = GetComponent<SerVivoStats>();
	}

    void Update()
    {
        PersonagemCombate playerCombat = ControladorPersonagem.instancia.GetComponent<PersonagemCombate>();
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
