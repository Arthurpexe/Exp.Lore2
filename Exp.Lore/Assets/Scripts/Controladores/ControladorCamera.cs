using UnityEngine;

public class ControladorCamera : MonoBehaviour
{
    ControladorPersonagem controladorPersonagem;
    public Transform posCamBoss;
    ModoCamera modoCamera;

    private void Start()
    {
        controladorPersonagem = ControladorPersonagem.instancia;
        modoCamera = ModoCamera.seguePlayer;
    }

    public void Update()
    {
        switch(modoCamera)//controla o que a camera vai seguir
        {
            case ModoCamera.seguePlayer:
                    seguePlayer();
                break;

            case ModoCamera.fixaBoss:
                    fixaBoss();
                break;
            default:
                seguePlayer();
                break;
        }
    }
    /// <summary>
    /// A câmera segue o player(mas não se movimenta[AINDA])
    /// </summary>
    public void seguePlayer()
    {
        modoCamera = ModoCamera.seguePlayer;
        transform.position = Vector3.Lerp(transform.position, new Vector3(controladorPersonagem.transform.position.x, controladorPersonagem.transform.position.y + 14f, controladorPersonagem.transform.position.z + 7f), 6 * Time.deltaTime);
        transform.rotation = Quaternion.Euler(50f, 180f, 0f);
    }
    /// <summary>
    /// A câmera fica fixa posicionada na area do boss de um angulo que de pra ver toda a batalha
    /// </summary>
    public void fixaBoss()
    {
        modoCamera = ModoCamera.fixaBoss;
        transform.position = posCamBoss.position;
        transform.rotation = posCamBoss.rotation;
    }

    public enum ModoCamera{seguePlayer, fixaBoss}
}
