using UnityEngine;

public class ControladorCamera : MonoBehaviour
{
	public Transform personagem;
    public Transform posCamBoss;
    public bool dentroAreaBoss;

    void Update()
    {
        if (dentroAreaBoss)
        {
            transform.position = posCamBoss.position;
            transform.rotation = posCamBoss.rotation;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(personagem.position.x, personagem.position.y + 14f, personagem.position.z + 7f), 6 * Time.deltaTime);
            transform.rotation = Quaternion.Euler(50f, 180f, 0f);
        }
    }

}
