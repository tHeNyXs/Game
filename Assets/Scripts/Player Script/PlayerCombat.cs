using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public float weaponRange = 1f;
    public LayerMask enemyLayer;
    public int damage = 1;
    public Animator anim;

    public float cooldown = 2f;
    private float timer;

    private void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);

            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, enemyLayer);
            if (enemies.Length > 0)
            {
                enemies[0].GetComponent<EnemyHealth>().ChangeHealth(-damage);
            }

            timer = cooldown;
        }
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }
}
