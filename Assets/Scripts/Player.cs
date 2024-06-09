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
    public int defendDamageReduction = 5; // ��� �� �޴� ������ ���ҷ�
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
        // �� ���� ó���� �߰��� �� �ֽ��ϴ�.
    }

    public void TakeDamage(int damage)
    {
        // ��� ������ ��� ������ ����
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
