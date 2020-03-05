using UnityEngine;

public class Interagivel : MonoBehaviour
{
	[SerializeField]
	protected float raio = 1f;
	protected GameObject player;
    protected ControladorPersonagem controladorPersonagem;

	private void Start()
	{
		controladorPersonagem = ControladorPersonagem.instancia;
		player = GameObject.FindWithTag("Player");
	}

	public void Interact()
	{	
		Debug.Log("Interagiu");
	}
	
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, raio);
	}
}
