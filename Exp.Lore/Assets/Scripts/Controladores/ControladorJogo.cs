using UnityEngine;

public class ControladorJogo : MonoBehaviour
{
    public SaveLoad saveLoad;
    void Start()
    {
        saveLoad = new SaveLoad(GameObject.FindWithTag("Player"));
    }


    public void salvarJogo()
    {
        saveLoad.salvarPlayer();
    }
    public void carregarJogo()
    {
        saveLoad.carregarPlayer();
    }
    public void sairJogo()
    {
        Application.Quit();
    }
}
