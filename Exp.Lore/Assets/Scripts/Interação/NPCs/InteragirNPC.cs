using UnityEngine;
using UnityEngine.UI;

public class InteragirNPC : Interagivel
{
    [SerializeField]
    protected string nomeNPC;
    public Text textoNomeNPCDialogo;
    public GameObject painelDialogo;
    protected Text textoDialogo;
    [SerializeField]
    protected string dialogo;

    InstanciarBotao instanciarBotao;
    protected virtual void Start()
    {
        textoDialogo = painelDialogo.GetComponentInChildren<Text>();
        instanciarBotao = new InstanciarBotao();
    }

    protected virtual void Update()
    {
        if (instanciarBotao.instanciarBotaoPorProximidade(transform, raio) && Input.GetButtonDown("Interagir"))
        {
            Interact();
        }
    }

    public virtual void Interact()
    {
        textoNomeNPCDialogo.text = nomeNPC;
        textoDialogo.text = dialogo;
        painelDialogo.SetActive(!painelDialogo.activeSelf);
    }
}
