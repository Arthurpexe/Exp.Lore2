using UnityEngine;
using UnityEngine.UI;

public class InventarioSlot : MonoBehaviour
{    
    public Image icone;
    public Image botaoExcluirImagem;

	public Item item;

    public void adicionarItem (Item newItem)
	{
		item = newItem;

		icone.sprite = item.getIcone();
		icone.enabled = true;
        if(botaoExcluirImagem != null)
            botaoExcluirImagem.enabled = true;
        Debug.Log(Inventario.instance.imprimirNomeDosItens());
    }

	public void limparSlot ()
	{
		item = null;

		icone.sprite = null;
		icone.enabled = false;
        if (botaoExcluirImagem != null)
            botaoExcluirImagem.enabled = false;
	}

    public void apertarBotaoExcluir()
    {
        Debug.Log(Inventario.instance.imprimirNomeDosItens());
        Inventario.instance.remover(item);
        Debug.Log(Inventario.instance.imprimirNomeDosItens());

    }

    public void usarItem()
	{
		if (item != null)
		{
			item.Use();
		}
	}

    public void desequipar()
    {
        if (item != null)
        {
            item.desequipar();
        }
    }
}
