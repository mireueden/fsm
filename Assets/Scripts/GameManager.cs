using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Enemy enemy;

    bool isCombatInProgress = false; // 전투 진행 여부
    public float actionDelay = 1.0f; // 각 행동 사이의 딜레이 시간

    void Start()
    {
        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();

        // 전투 시작
        StartCombat();
    }

    void StartCombat()
    {
        // 전투 시작 메시지 출력
        Debug.Log("Battle starts!");

        isCombatInProgress = true;

        // 전투 진행 코루틴 시작
        StartCoroutine(CombatCoroutine());
    }

    IEnumerator CombatCoroutine()
    {
        while (isCombatInProgress)
        {
            // 플레이어와 적의 상태를 결정
            DeterminePlayerState();
            DetermineEnemyState();

            // 플레이어와 적의 행동
            PerformPlayerAction();
            PerformEnemyAction();

            // 적이 살아있는지 확인하고 죽었으면 전투 종료
            if (enemy.currentState == EnemyState.Dead)
            {
                Debug.Log("Enemy is defeated. Victory!");
                isCombatInProgress = false; // 전투 종료
                yield break;
            }

            // 플레이어가 살아있는지 확인하고 죽었으면 전투 종료
            if (player.currentState == PlayerState.Dead)
            {
                Debug.Log("Player is defeated. Game Over!");
                isCombatInProgress = false; // 전투 종료
                yield break;
            }

            yield return new WaitForSeconds(actionDelay);
        }
    }

    void DeterminePlayerState()
    {
        // 플레이어의 상태를 무작위로 결정
        int randomState = Random.Range(0, 2); // 0: Attack, 1: Defend
        player.currentState = (randomState == 0) ? PlayerState.Attack : PlayerState.Defend;
    }

    void DetermineEnemyState()
    {
        // 적의 상태를 무작위로 결정
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
