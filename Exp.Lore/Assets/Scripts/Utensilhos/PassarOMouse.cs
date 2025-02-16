﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PassarOMouse : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public GameObject painelDescricaoItem;
    public Transform localPainelDescricaoItem;

    Item item;

    public void OnPointerEnter(PointerEventData eventData)
    {
        item = gameObject.GetComponentInParent<InventarioSlot>().item;
        if (item != null)
        {
            painelDescricaoItem.transform.GetChild(0).GetComponent<Text>().text = item.getNome();
            painelDescricaoItem.transform.GetChild(1).GetComponent<Text>().text = item.getDescricao();
            painelDescricaoItem.transform.GetChild(5).GetComponent<Text>().text = item.getPreco().ToString();
            Instantiate(painelDescricaoItem, localPainelDescricaoItem);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (localPainelDescricaoItem.childCount > 0) 
            Destroy(localPainelDescricaoItem.GetChild(0).gameObject);
    }
}
