using UnityEngine;

public class Teleport : MonoBehaviour
{
	public Transform saida;
	GameObject player;

	private void Start()
	{
		player = ControladorPersonagem.instancia.gameObject;
	}
	private void OnTriggerEnter(Collider other)
	{
		player.transform.position = saida.transform.position;
	}
}