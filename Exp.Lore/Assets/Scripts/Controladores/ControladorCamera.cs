using UnityEngine;

public class ControladorCamera : MonoBehaviour
{
    Transform personagemTrans;
    Transform cameraTrans;
    public Transform posCamBoss;

    ModoCamera modoCamera;

    [SerializeField]
    float sensibilidadeMouse = 300;

    private void Start()
    {
        personagemTrans = ControladorPersonagem.instancia.transform;
        modoCamera = ModoCamera.seguePlayer;
        cameraTrans = transform.GetChild(0);
    }

    private void Update()
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

        //segue a posição do player
        transform.position = Vector3.Lerp(transform.position, personagemTrans.position, 6 * Time.deltaTime);

        //rotaciona a camera na orbita do player
        float rotX = Input.GetAxis("Mouse X") * sensibilidadeMouse * Time.fixedDeltaTime;
        cameraTrans.RotateAround(transform.position , Vector3.up, rotX);
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
