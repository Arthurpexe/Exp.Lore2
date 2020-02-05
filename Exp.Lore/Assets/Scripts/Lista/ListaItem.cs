using UnityEngine;

public class ListaItem{
	
	public ElementoListaItem primeiro, ultimo;
    public int contador;
	
	public ListaItem(){//cria uma nova lista

        primeiro = new ElementoListaItem(null);
        ultimo = primeiro;
        
        contador = 0;
	}
	
	public void inserir(Item novoItem){//insere um novo dado no final da lista 

        ElementoListaItem novoElemento = new ElementoListaItem(novoItem);

        ultimo.proximo = novoElemento;
        ultimo = novoElemento;

        contador++;
        novoElemento.endereco = ultimo.endereco = contador;
    }
	
	public Item retirar(Item itemRetirado){//retorna o dado, seja ele qual for, para o programa processa-lo como quiser 

        ElementoListaItem aux;

		aux = localizarPorItem(itemRetirado);

        if (aux == null)
        {
            Debug.Log("tentando retirar o sentinela");
            return null;
        }

        ElementoListaItem auxRet = aux.proximo;

        if (auxRet == ultimo){
            aux.proximo = null;
            ultimo = aux;
		}
        else
        {
            aux.proximo = auxRet.proximo;
        }
        auxRet.proximo = null;

        contador--;
        ultimo.endereco = contador;

		return auxRet.meuItem;
	}
	
	public ElementoListaItem localizarPorEndereco(int endereco)//localiza o elemento, e o retorna, sem fazer nada com ele
    {
		if(vazia())
			return null;
		
		ElementoListaItem aux = primeiro;

		while (aux != null && aux.endereco != endereco) 
        { 
			aux = aux.proximo;
		}
        return aux;
	}

    public ElementoListaItem localizarPorItem(Item itemRetirado)//localiza o elemento, e o retorna, sem fazer nada com ele
    {
        if (vazia())
            return null;

        ElementoListaItem aux = primeiro;

        while (aux.proximo != null && aux.proximo.meuItem != itemRetirado)
        {
            aux = aux.proximo;
        }
        return aux;
    }

    public bool vazia(){//checa se a lista esta vazia
		if(primeiro.proximo == null)
			return true;
		else
			return false;
	}

    public Item[] imprimirLista()
    {
        Item[] itens = new Item[contador];
        ElementoListaItem aux = primeiro.proximo;
        for(int i = 0; i < contador; i++)
        {
            itens[i] = aux.meuItem;
            aux = aux.proximo;
        }
        return itens;
    }
}

	