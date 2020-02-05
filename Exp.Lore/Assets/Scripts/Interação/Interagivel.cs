
using UnityEngine;

public class Interagivel : MonoBehaviour
{
	public float radius = 1f;
	public Transform interactionTransform;
	
	public GameObject player;

    public ControladorPersonagem controladorPersonagem;

    private void Start()
    {
        controladorPersonagem = ControladorPersonagem.instancia;
    }

    public virtual void Interact()
	{
		Debug.Log("CONSEGUIU");
	}
	


	private void OnDrawGizmosSelected()
	{
		if (interactionTransform == null)
			interactionTransform = transform;

		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(interactionTransform.position, radius);
	}

}
