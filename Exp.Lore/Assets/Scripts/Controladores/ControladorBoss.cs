using UnityEngine;

public class ControladorBoss : ControladorInimigos
{
	RaycastHit alvo;

	protected override void idle()
	{
	}
	protected override void dentroRaioDeVisao()
	{
		anim.SetBool("dentroDoRangeVisao", true);
	}
	protected override void atacar()
	{
		Debug.Log("Vou atacar!");

		anim.SetTrigger("atacar");
		cooldownAtaqueAtual = cooldownAtaqueMax;
	}

	public void disparar(SerVivoStats alvoStats)
	{
		Physics.SphereCast(transform.position + Vector3.down * 2, 1, transform.forward * 10, out alvo);
		if (alvo.transform.name == "Personagem")
		{
			alvoStats.TomarDano(meusStats.getDano());
			cooldownAtaqueAtual = cooldownAtaqueMax;
		}
	}
	protected override void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();

		Gizmos.color = Color.blue;//tudo que ta pintado de azul é pra representar a visão do inimigo, mas não tá implementado, apenas desenhado
		Gizmos.DrawRay(transform.position, Vector3.forward * raioDeVisao * 1.15f);
		Gizmos.DrawRay(transform.position, new Vector3(0.7f, 0, 0.7f) * raioDeVisao * 1.15f);
		Gizmos.DrawRay(transform.position, new Vector3(-0.7f, 0, 0.7f) * raioDeVisao * 1.15f);
		Gizmos.DrawRay(transform.position, new Vector3(0.5f, 0, 0.86f) * raioDeVisao * 1.15f);
		Gizmos.DrawRay(transform.position, new Vector3(-0.5f, 0, 0.86f) * raioDeVisao * 1.15f);
		Gizmos.DrawRay(transform.position, new Vector3(0.25f, 0, 0.96f) * raioDeVisao * 1.15f);
		Gizmos.DrawRay(transform.position, new Vector3(-0.25f, 0, 0.96f) * raioDeVisao * 1.15f);

		Gizmos.color = Color.green;// verde representa a mira de ataque do inimigo
		Gizmos.DrawRay(transform.position, transform.forward * raioDeAtaque);
	}
}