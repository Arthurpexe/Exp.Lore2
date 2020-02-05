using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ref_posiçao_jogador : MonoBehaviour
{
	#region Singleton

	public static Ref_posiçao_jogador instance;

	 void Awake()
	 {
		instance = this;
	 }

	#endregion

	public GameObject player;
    public GameObject painelMorte;

	public void MatarPlayer ()
	{
        painelMorte.SetActive(true);
        Time.timeScale = 0.1f;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
