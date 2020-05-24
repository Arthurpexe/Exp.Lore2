using UnityEngine;

public class MorrerSMB : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<SerVivoStats>().Morrer();
    }
}
