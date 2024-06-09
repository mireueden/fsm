using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Attack,
    Defend,
    Dead
}

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int attackDamage = 10;
    public int defendDamageReduction = 5; // 방어 시 받는 데미지 감소량
    public EnemyState currentState;

    void Start()
    {
        currentState = EnemyState.Idle;
    }

    public void Attack(Player player)
    {
        Debug.Log("Enemy attacks!");
        player.TakeDamage(attackDamage);
    }

    public void Defend()
    {
        Debug.Log("Enemy is defending!");
        // 방어에 따른 처리를 추가할 수 있습니다.
    }

    public void TakeDamage(int damage)
    {
        // 방어 상태인 경우 데미지 감소
        if (currentState == EnemyState.Defend)
        {
            damage -= defendDamageReduction;
            if (damage < 0)
                damage = 0;
        }

        health -= damage;
        Debug.Log("Enemy takes " + damage + " damage. Remaining health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy is defeated!");
        currentState = EnemyState.Dead;
    }
}
