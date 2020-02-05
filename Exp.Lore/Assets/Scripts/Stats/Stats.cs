using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Stats 
{

	[SerializeField]
	private int valorBase = 0;

	private List<int> modificadores = new List<int>();

	
public int PegarValor()
	{
		int valorFinal = valorBase;
		modificadores.ForEach(x => valorFinal += x);
		return valorBase;
	}

    public void AdicionarModificador(int modificador)
	{
		if (modificador != 0)
			modificadores.Add(modificador);
	}

	public void RemoverModificador(int modificador)
	{
		if (modificador != 0)
			modificadores.Remove(modificador);
	}

}
