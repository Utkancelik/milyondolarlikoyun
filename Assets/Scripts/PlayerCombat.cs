using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;
    public int attackDamage = 20;


    public float attackRate = 2.0f;
    private float nextAttackTime = 0.0f;

    public int maxHealth = 100;
    public int currentHealth;

    public bool isDead = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Time.time > nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        else
        {
            animator.ResetTrigger("Attack");
        }
        
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyClass = enemy.GetComponent<Enemy>();
            enemyClass.TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        animator.SetBool("isDead", true);

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        this.enabled = false;
    }
}
