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
		Inventario.instance.remover(this);
        Debug.Log("removi " + nome);
	}

    public string getNome() { return nome; }
    public string getDescricao() { return descricao; }
    public int getPreco() { return preco; }
    public Sprite getIcone() { return icone; }
    public bool getIsDefaultItem() { return isDefaultItem; }
}
