using UnityEngine;

public class MovimentoPersonagem
{
    float velocidadeCorrendo;
    float velocidadeAbaixado;
    float velocidade;
    float distanciaRolamento;
    Rigidbody personagemRB;
    bool correndo;
    bool abaixado;
    Animator animator;
    Vector3 frente;

    public MovimentoPersonagem(Rigidbody rb, Animator anim)
    {
        velocidadeCorrendo = 2f;
        velocidadeAbaixado = 0.5f;
        velocidade = 5f;
        distanciaRolamento = 2;
        personagemRB = rb;
        correndo = false;
        abaixado = false;
        animator = anim;
        frente = personagemRB.transform.forward;
    }

    public void movePosicao(Vector3 inputs)
    {
        personagemRB.MovePosition(personagemRB.position - inputs * velocidade * Time.fixedDeltaTime);
    }

    public void andar(Vector3 inputs)
    {
        animator.SetFloat("mov", 1);
        frente = -inputs;
        personagemRB.transform.forward = frente;
    }
    public void correr()
    {
        if (!correndo)
        {
            correndo = true;
            animator.SetFloat("mov", 2);
            animator.SetBool("agachado", false);
        }
    }
    public void parar()
    {
        animator.SetFloat("mov", 0);
        correndo = false;
    }
    public void abaixar()
    {
        if (!abaixado)
        {
            abaixado = true;
            animator.SetBool("agachado", true);
        }
        else
        {
            abaixado = false;
            animator.SetBool("agachado", false);
        }
    }
    public void rolar()
    {
        animator.SetTrigger("rolar");
        Vector3 velocidadeRolamento = Vector3.Scale(frente, distanciaRolamento * new Vector3(5, 0, 5));
        personagemRB.AddForce(velocidadeRolamento, ForceMode.VelocityChange);
    }


    public object info(TipoInformacao t)
    {
        switch (t)
        {
            case TipoInformacao.velocidadeCorrendo:
                return velocidadeCorrendo;
            case TipoInformacao.velocidadeAbaixado:
                return velocidadeAbaixado;
            case TipoInformacao.correndo:
                return correndo;
            case TipoInformacao.abaixado:
                return abaixado;
            default:
                return null;
        }
    }

    public enum TipoInformacao { velocidadeCorrendo, velocidadeAbaixado, correndo, abaixado }
}
