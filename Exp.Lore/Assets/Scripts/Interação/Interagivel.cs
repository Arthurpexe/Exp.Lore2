using UnityEngine;

public class Interagivel : MonoBehaviour
{
	[SerializeField]
	protected float raio = 1f;
	protected GameObject player;

	protected virtual void Start()
	{
		player = GameObject.FindWithTag("Player");
	}

	protected virtual void Interact() { }
	
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, raio);
	}
}
