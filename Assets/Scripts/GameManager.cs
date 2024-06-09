using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Enemy enemy;

    bool isCombatInProgress = false; // ���� ���� ����
    public float actionDelay = 1.0f; // �� �ൿ ������ ������ �ð�

    void Start()
    {
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();

        // ���� ����
        StartCombat();
    }

    void StartCombat()
    {
        // ���� ���� �޽��� ���
        Debug.Log("Battle starts!");

        isCombatInProgress = true;

        // ���� ���� �ڷ�ƾ ����
        StartCoroutine(CombatCoroutine());
    }

    IEnumerator CombatCoroutine()
    {
        while (isCombatInProgress)
        {
            // �÷��̾�� ���� ���¸� ����
            DeterminePlayerState();
            DetermineEnemyState();

            // �÷��̾�� ���� �ൿ
            PerformPlayerAction();
            PerformEnemyAction();

            // ���� ����ִ��� Ȯ���ϰ� �׾����� ���� ����
            if (enemy.currentState == EnemyState.Dead)
            {
                Debug.Log("Enemy is defeated. Victory!");
                isCombatInProgress = false; // ���� ����
                yield break;
            }

            // �÷��̾ ����ִ��� Ȯ���ϰ� �׾����� ���� ����
            if (player.currentState == PlayerState.Dead)
            {
                Debug.Log("Player is defeated. Game Over!");
                isCombatInProgress = false; // ���� ����
                yield break;
            }

            yield return new WaitForSeconds(actionDelay);
        }
    }

    void DeterminePlayerState()
    {
        // �÷��̾��� ���¸� �������� ����
        int randomState = Random.Range(0, 2); // 0: Attack, 1: Defend
        player.currentState = (randomState == 0) ? PlayerState.Attack : PlayerState.Defend;
    }

    void DetermineEnemyState()
    {
        // ���� ���¸� �������� ����
        int randomState = Random.Range(0, 2); // 0: Attack, 1: Defend
        enemy.currentState = (randomState == 0) ? EnemyState.Attack : EnemyState.Defend;
    }

    void PerformPlayerAction()
    {
        if (player.currentState == PlayerState.Attack)
            player.Attack(enemy);
        else if (player.currentState == PlayerState.Defend)
            player.Defend();
    }

    void PerformEnemyAction()
    {
        if (enemy.currentState == EnemyState.Attack)
            enemy.Attack(player);
        else if (enemy.currentState == EnemyState.Defend)
            enemy.Defend();
    }
}
