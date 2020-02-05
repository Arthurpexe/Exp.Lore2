using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Boss1_Ataque : StateMachineBehaviour
{
	public GameObject Boss;
	private Boss1Combate script;
	private PersonagemStats script2;
	public GameObject player;
	
	
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		Boss = GameObject.Find("Boss");
		script = Boss.GetComponent<Boss1Combate>();
		player = GameObject.Find("Personagem");
		script2 = player.GetComponent<PersonagemStats>();
		//playerStats = player.GetComponent<PersonagemStats>();

	}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		script.Disparar(script2);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
