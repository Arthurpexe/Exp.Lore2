using UnityEngine;

public class FSM_Player_Morrer : StateMachineBehaviour
{
	GameObject controlador;
	ControladorJogo morre;
	
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		controlador = GameObject.Find("ControladorGeral");
		morre = controlador.GetComponent<ControladorJogo>();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		morre.painelFimDeJogo.SetActive(true);
	}
}
