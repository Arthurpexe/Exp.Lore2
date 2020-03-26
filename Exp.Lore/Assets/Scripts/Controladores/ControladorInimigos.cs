using UnityEngine;

public abstract class ControladorInimigos : MonoBehaviour
{
    [SerializeField]
    protected float raioDeVisao = 10;
    [SerializeField]
    protected float raioDeAtaque = 3;

    protected Transform target;
    protected SerVivoStats jogadorStats;
    protected SerVivoStats meusStats;
    protected Animator anim;

    protected float distancia;
    protected float cooldownAtaqueAtual = 0;
    [SerializeField]
    protected float cooldownAtaqueMax = 2;
    [SerializeField]
    protected float ataqueDelay = .6f;

    protected virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        jogadorStats = target.GetComponent<SerVivoStats>();
        meusStats = GetComponent<SerVivoStats>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        cooldownAtaqueAtual -= Time.deltaTime;
        distancia = Vector3.Distance(target.position, transform.position);

        if (distancia <= raioDeVisao)
        {
            olharParaAlvo();
            dentroRaioDeVisao();
            if (distancia <= raioDeAtaque && cooldownAtaqueAtual <= 0)
            {
                atacar();
            }
        }
        else
        {
            idle();
        }
    }
    private void olharParaAlvo()
    {
        Vector3 direcao = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direcao.x, 0, direcao.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    protected abstract void idle();
    protected abstract void dentroRaioDeVisao();
    protected abstract void atacar();

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;//amarelo é a atual visão implementada do inimigo, que deveria ser an verdade apenas a audição dele
        Gizmos.DrawWireSphere(transform.position, raioDeVisao);

        Gizmos.color = Color.red;//vermelho é o raio de ataque do inimigo, apartir dali ele consegue atacar o personagem
        Gizmos.DrawWireSphere(transform.position, raioDeAtaque);
    }
}
