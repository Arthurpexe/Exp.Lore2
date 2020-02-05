
using UnityEngine;

[CreateAssetMenu(fileName = "Novo Item", menuName = "Inventário/Item")]
public class Item : ScriptableObject
{
    public string nome = "Novo Item";
    public string descricao = "Descrição";
    public int preco = 0;
    public Sprite icon = null;
    public bool isDefaultItem = false;
	
    public virtual void Use()
    {
        //usar o item
    }

    public virtual void desequipar()
    {
        //dessequipar o item
    }

    public void RemoverDoInventario()
	{
		Inventario.instance.Remove(this);
        Debug.Log("removi " + this.nome);
	}

}
