using UnityEngine;

public class ControladorCamera : MonoBehaviour
{
    Transform personagemTrans;
    Transform cameraTrans;
    public Transform posCamBoss;
    Vector3 posInicial;

    ModoCamera modoCamera;

    [SerializeField]
    float sensibilidadeMouse = 300;

    private void Start()
    {
        personagemTrans = ControladorPersonagem.instancia.transform;
        modoCamera = ModoCamera.seguePlayer;
        cameraTrans = transform.GetChild(0);
        posInicial = cameraTrans.localPosition;
    }

    private void Update()
    {
        switch (modoCamera)//controla o que a camera vai seguir
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
    /// A câmera segue o player e rotaciona em sua orbita através do movimento do mouse
    /// </summary>
    public void seguePlayer()
    {
        //segue a posição do player
        transform.position = Vector3.Lerp(transform.position, personagemTrans.position + new Vector3(0,7,0), 6 * Time.deltaTime);

        if (Cursor.lockState != CursorLockMode.None)
        {
            //rotaciona a camera na orbita do player
            float rotX = Input.GetAxis("Mouse X") * sensibilidadeMouse * Time.fixedDeltaTime;
            cameraTrans.RotateAround(transform.position, Vector3.up, rotX);
        }
    }
    /// <summary>
    /// A câmera fica fixa posicionada na area do boss de um angulo que de pra ver toda a batalha
    /// </summary>
    public void fixaBoss()
    {
        cameraTrans.position = posCamBoss.position;
        cameraTrans.rotation = posCamBoss.rotation;
    }

    public void mudaCamera(ModoCamera modo)
    {
        switch (modo)
        {
            case ModoCamera.seguePlayer:
                if (modoCamera == ModoCamera.fixaBoss)
                {
                    cameraTrans.localPosition = posInicial;
                }
                modoCamera = ModoCamera.seguePlayer;
                seguePlayer();
                break;
            case ModoCamera.fixaBoss:
                modoCamera = ModoCamera.fixaBoss;
                break;
            default:
                seguePlayer();
                break;
        }
    }

    public enum ModoCamera { seguePlayer, fixaBoss }
}
