using UnityEngine;

public class ItemPickUp : Interagivel
{
	[SerializeField]
	Item item;

	private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= raio)
        {
            Interact();
        }
    }

    protected override void Interact()
	{
		base.Interact();
        Debug.Log("Interagindo com " + item.getNome());
		pickUp();
	}

	private void pickUp()
	{
		Debug.Log("Pegando " + item.getNome());
		bool wasPickedUp = Inventario.instance.adicionar(item);

		if(wasPickedUp)
		  Destroy(gameObject);
	}
}
