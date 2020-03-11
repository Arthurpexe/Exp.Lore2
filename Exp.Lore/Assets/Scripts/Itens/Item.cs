using UnityEngine;

[CreateAssetMenu(fileName = "Novo Item", menuName = "Inventário/Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    protected string nome = "Novo Item";
    [SerializeField]
    protected string descricao = "Descrição";
    [SerializeField]
    protected int preco = 0;
    [SerializeField]
    protected Sprite icone = null;
    [SerializeField]
    protected bool isDefaultItem = false;
	
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
        Debug.Log("removi " + nome);
	}

    /// <summary>
    /// Retorna todas as informações do Item, é nescessario especificar qual o tipo a ser convertido pois info() retorna um object.
    /// </summary>
    /// <param name="ti">Item.TipoInformação</param>
    /// <returns></returns>
    public object getInfo(Tipo ti)
    {
        switch (ti)
        {
            case Tipo.nome:
                return nome;
            case Tipo.descricao:
                return descricao;
            case Tipo.preco:
                return preco;
            case Tipo.icone:
                return icone;
            case Tipo.isDefaultItem:
                return isDefaultItem;
            default:
                return null;
        }
    }

    public enum Tipo { nome, descricao, preco, icone, isDefaultItem}
}
