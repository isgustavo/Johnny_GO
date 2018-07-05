using ODT.GO.Board;
using System;
using UnityEngine;

namespace ODT.GO.Enemy
{
    [RequireComponent(typeof (EnemyMoveBehaviour))]
    public class EnemySensorBehaviour : MonoBehaviour
    {
        private EnemyMoveBehaviour enemyMove;

        private void OnEnable()
        {
            enemyMove = GetComponent<EnemyMoveBehaviour>();
        }
        /*
        public void OnPlayerMove()
        {
            if (BoardBehaviour.instancie.IsPlayerOnMySide(enemyMove.LookAtNode))
            {
                Debug.Log("ATTACK!");
            }
            else
            {
               
            }
        }*/
    }
}
