using UnityEngine;

public class InimigoStats : SerVivoStats
{
	GameObject item;

	public override void MorrerAnimacao()
	{
		if (item != null)
			Instantiate(item, this.transform.position + Vector3.up, Quaternion.identity);

		anim.SetTrigger("Morrer");
	}

	public override void Morrer()
	{
		Destroy(gameObject);
	}
}
