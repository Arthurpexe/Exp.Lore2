using System.Text;
using UnityEngine;

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

    [SerializeField]
    int espaco = 20;

    [SerializeField]
    ListaItem listaItens;

    private void Start()
    {
        listaItens = new ListaItem();
    }

    public bool adicionar(Item item)
    {
        if (!item.getIsDefaultItem())
        {
            if (listaItens.getContador() >= espaco)
            {
                Debug.Log("Sem espaço suficiente no inventário.");
                return false;

            }
            listaItens.inserir(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void remover(Item item)
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
        for (int i = 0; i < vItens.Length; i++)
        {
            aux.Append(i + "° " + vItens[i].getNome() + ". ");
        }
        return aux.ToString();
    }
    public ListaItem getListaItens() { return listaItens; }
}
