using UnityEngine;
public class MovimentoPersonagem
{ 
    float velocidadeCorrendo = 18;
    float velocidadeAbaixado = 3;
    float velocidade = 9;
    float distanciaRolamento = 15;
    Rigidbody personagemRB;
    bool correndo;
    bool abaixado;
    Animator animator;

    Transform cameraTrans;
    Vector3 frente, direita;
    Vector3 direcao;
    Vector3 direcaoRolamento;

    public MovimentoPersonagem(Rigidbody rb, Animator anim)
    {
        personagemRB = rb;
        correndo = false;
        abaixado = false;
        animator = anim;

        cameraTrans = GameObject.FindGameObjectWithTag("MainCamera").transform.GetChild(0);
        frente = new Vector3(cameraTrans.forward.x,0,cameraTrans.forward.z);
        direita = new Vector3(cameraTrans.right.x, 0, cameraTrans.right.z);
    }

    public void movePosicao(Vector3 inputs)
    {
        frente = new Vector3(cameraTrans.forward.x, 0, cameraTrans.forward.z);
        direita = new Vector3(cameraTrans.right.x, 0, cameraTrans.right.z);

        direcao = Vector3.zero;
        //define a direção do movimento do personagem
        if(inputs.z > 0)
        {
            direcao += frente;
        }
        if(inputs.z < 0)
        {
            direcao += -frente;
        }
        if (inputs.x > 0)
        {
            direcao += direita;
        }
        if (inputs.x < 0)
        {
            direcao += -direita;
        }
        //se estiver andando na diagonal diminui sua velocidade
        if(inputs.z != 0 && inputs.x != 0)
        {
            direcao /= 1.5f;
        }
        //modificadores de velocidade por estados
        if (correndo)
        {
            direcao *= velocidadeCorrendo;
        }
        else if (abaixado)
        {
            direcao *= velocidadeAbaixado;
        }
        else
        {
            direcao *= velocidade;
        }
        personagemRB.MovePosition(personagemRB.transform.position + direcao * Time.fixedDeltaTime);

        //se a velocidade do personagem for menor que um certo limite a variavel direcaoRolamento não vai pegar o valor de direcao para que ele sempre tenha uma direcao pra rolar mesmo que parado.
        if(direcao.normalized.x > 0.5f || direcao.normalized.x < -0.5f || direcao.normalized.z > 0.5f || direcao.normalized.z < -0.5f)
        {
            direcaoRolamento = direcao.normalized;
        }
    }

    public void andar(Vector3 inputs)
    {
        animator.SetFloat("mov", 1);

        personagemRB.transform.forward = (direcao + frente) * Time.fixedDeltaTime;
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

        Vector3 velocidadeRolamento = Vector3.Scale(direcaoRolamento, distanciaRolamento * new Vector3(1, 0, 1));
        personagemRB.AddForce(velocidadeRolamento, ForceMode.VelocityChange);
    }
}
