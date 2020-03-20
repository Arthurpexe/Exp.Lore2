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

    public void Interact()
	{
		base.Interact();
        Debug.Log("Interagindo com " + item.getNome());
		pickUp();
	}

	public void pickUp()
	{
		Debug.Log("Pegando " + item.getNome());
		bool wasPickedUp = Inventario.instance.adicionar(item);

		if(wasPickedUp)
		  Destroy(gameObject);
	}
}
