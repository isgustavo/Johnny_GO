using ODT.GO.Board;
using ODT.GO.Enemy;
using ODT.GO.Player;
using ODT.Util.Scriptable;
using System;
using UnityEngine;

namespace ODT.GO
{
    [Serializable]
    public enum Turn
    {
        Player,
        Enemy
    }

    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instancie;

        [Header("Player")]
        [SerializeField]
        private GameObject playerPrefab;
        [Header("Enemy")]
        [SerializeField]
        private GameObject enemyPrefab;
        [SerializeField]
        private EnemyOnBoard[] enemiesOnBoard;
        [Header("Events")]
        private GameEvent OnTurnChanged;

        private BoardBehaviour board;
        private PlayerMoveBehaviour player;
        private EnemyMoveBehaviour[] enemies;

        private Turn currentTurn;
        public Turn CurrentTurn
        {
            get
            {
                return currentTurn;
            }
        }

        private void Awake()
        {
            if (instancie == null)
            {
                instancie = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            InstantiateBoard();
            InstantiatePlayer();
            enemies = new EnemyMoveBehaviour[enemiesOnBoard.Length];
            InstantiateEnemies();
        }

        private void InstantiateBoard()
        {
            board = GetComponent<BoardBehaviour>();
        }

        private void InstantiatePlayer()
        {
            Vector3 position = new Vector3(board.FirstNode.transform.position.x, 1.5f, board.FirstNode.transform.position.z);
            var playerObj = Instantiate(playerPrefab, position, Quaternion.identity);
            player = playerObj.GetComponent<PlayerMoveBehaviour>();
            player.SetCurrentNode(board.FirstNode);
        }

        private void InstantiateEnemies()
        {
            for (int i = 0; i < enemiesOnBoard.Length; i++)
            {
                Vector3 position = new Vector3(enemiesOnBoard[i].InitNodePosition.transform.position.x, 1.5f, enemiesOnBoard[i].InitNodePosition.transform.position.z);
                GameObject enemyObj = Instantiate(enemyPrefab, position, Quaternion.identity);
                EnemyMoveBehaviour newEnemy = enemyObj.GetComponent<EnemyMoveBehaviour>();
                newEnemy.SetCurrentNode(enemiesOnBoard[i].InitNodePosition);
                newEnemy.SetLookAtNode(enemiesOnBoard[i].LookAtNodePosition);
                enemies[i] = newEnemy;
            }
        }

        public void OnPlayerMoved()
        {
            currentTurn = Turn.Enemy;

            MoveEnemies();
        }

        private void MoveEnemies()
        {
            if (enemies != null)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].MakeAMove();
                }
            }

            currentTurn = Turn.Player;
        }

        public bool IsPlayerOnSideTo(NodeBehaviour lookAtNode)
        {
            if (player.CurrentNode.transform.position == lookAtNode.transform.position)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
    [Serializable]
    public struct EnemyOnBoard
    {
        public NodeBehaviour InitNodePosition;
        public NodeBehaviour LookAtNodePosition;
    }
}
