using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCameraMapa : MonoBehaviour
{
    public GameObject Personagem;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Personagem.transform.position.x, Personagem.transform.position.y + 150, Personagem.transform.position.z), 6 * Time.deltaTime);
    }
}
