using UnityEngine;

public class AtaqueBoss : StateMachineBehaviour
{
	ControladorBoss controladorBoss;
	SerVivoStats statsPersonagem;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controladorBoss = animator.GetComponentInParent<ControladorBoss>();
        statsPersonagem = ControladorPersonagem.instancia.getSerVivoStats();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		controladorBoss.disparar(statsPersonagem);
    }
}
