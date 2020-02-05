public class ElementoListaItem{
	
	public ElementoListaItem proximo;
	public Item meuItem;
    public int endereco;
	
	public ElementoListaItem(Item novoItem){//cria um novo elemento
		proximo = null;
		meuItem = novoItem;
        endereco = 0;
	}
}