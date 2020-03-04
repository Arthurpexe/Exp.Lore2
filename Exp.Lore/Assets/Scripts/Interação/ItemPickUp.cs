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
        Debug.Log("Interagindo com " + item.nome);
		pickUp();
	}

	public void pickUp()
	{
		Debug.Log("Pegando " + item.nome);
		bool wasPickedUp = Inventario.instance.Add(item);

		if(wasPickedUp)
		  Destroy(gameObject);
	}
}
