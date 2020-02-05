using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Inventario : MonoBehaviour
{
    #region Singleton
    public static Inventario instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Mais de uma instancia de Inventario encontrada!");
            return;
        }
        instance = this;

    }
    #endregion

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public int space = 20;

	public ListaItem listaItens;

    public void Start()
    {
        listaItens = new ListaItem();
    }
    public bool Add (Item item)
	{
		if (!item.isDefaultItem)
        {
            if (listaItens.contador >= space)
            {
                Debug.Log("Sem espaço suficiente no inventário.");
                return false;

            }
            listaItens.inserir(item);

			if(onItemChangedCallback != null)
			   onItemChangedCallback.Invoke();
		}
		return true;
	}

	public void Remove(Item item)
	{
		listaItens.retirar(item);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}
    public string imprimirNomeDosItens()
    {
        Item[] vItens;
        StringBuilder aux = new StringBuilder("Lista de Itens no inventario: ");
        vItens = listaItens.imprimirLista();
        for(int i = 0; i < vItens.Length; i++)
        {
            aux.Append(i+"° "+vItens[i].nome+". ");
        }
        return aux.ToString();
    }
}
