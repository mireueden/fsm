using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Attack,
    Defend,
    Dead
}

public class Player : MonoBehaviour
{
    public int health = 100;
    public int attackDamage = 10;
    public int defendDamageReduction = 5; // 방어 시 받는 데미지 감소량
    public PlayerState currentState;

    void Start()
    {
        currentState = PlayerState.Idle;
    }

    public void Attack(Enemy enemy)
    {
        Debug.Log("Player attacks!");
        enemy.TakeDamage(attackDamage);
    }

    public void Defend()
    {
        Debug.Log("Player is defending!");
        // 방어에 따른 처리를 추가할 수 있습니다.
    }

    public void TakeDamage(int damage)
    {
        // 방어 상태인 경우 데미지 감소
        if (currentState == PlayerState.Defend)
        {
            damage -= defendDamageReduction;
            if (damage < 0)
                damage = 0;
        }

        health -= damage;
        Debug.Log("Player takes " + damage + " damage. Remaining health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player is defeated!");
        currentState = PlayerState.Dead;
    }
}
