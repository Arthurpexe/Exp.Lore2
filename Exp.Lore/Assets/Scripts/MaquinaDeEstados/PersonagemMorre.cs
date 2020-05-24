using UnityEngine;

public class PersonagemMorre : StateMachineBehaviour
{
	GameObject controlador;
	ControladorPaineisHUD controladorPaineis;
	
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		controlador = GameObject.Find("HUDCanvas");
		controladorPaineis = controlador.GetComponent<ControladorPaineisHUD>();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		controladorPaineis.abrirPainel(controladorPaineis.painelFimDeJogo);
	}
}
