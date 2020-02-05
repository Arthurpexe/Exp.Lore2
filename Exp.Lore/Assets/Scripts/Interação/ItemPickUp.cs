
using UnityEngine;

public class ItemPickUp : Interagivel
{
	public Item item;

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, interactionTransform.position);
        if (distance <= radius)
        {

            Interact();
        }
    }

    public override void Interact()
	{
		base.Interact();
        Debug.Log("Interagindo com " + item.nome);
		PickUp();
	}

	public void PickUp()
	{
		Debug.Log("Pegando " + item.nome);
		bool wasPickedUp = Inventario.instance.Add(item);

		if(wasPickedUp)
		  Destroy(gameObject);
	}
}
