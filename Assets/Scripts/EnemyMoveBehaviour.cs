using ODT.GO.Board;
using ODT.GO.Util;
using ODT.Util.Scriptable;
using UnityEngine;

namespace ODT.GO.Enemy
{
    public interface IEnemy
    {
        void MakeAMove();
    }

    public class EnemyMoveBehaviour : MoveBehaviour, IEnemy
    {
        private NodeBehaviour lookAtNode;

        [SerializeField]
        private GameEvent OnCatchPlayerEvent;

        public void SetLookAtNode(NodeBehaviour node)
        {
            lookAtNode = node;
            LookAtRotation();
        }

        public void LookAtRotation ()
        {
            Vector3 position = lookAtNode.transform.position - transform.position;

            Quaternion rotation = Quaternion.LookRotation(position, Vector3.up);

            float newY = rotation.eulerAngles.y;

            iTween.RotateTo(gameObject, iTween.Hash(
                "y", newY,
                "delay", 0f,
                "easetype", easyType,
                "time", .25f
                ));
        }

        public void MakeAMove()
        {
            if (LevelManager.instancie.IsPlayerOnSideTo(lookAtNode))
            {
                MoveTo(lookAtNode);
                if (OnCatchPlayerEvent != null)
                {
                    OnCatchPlayerEvent.Raise();
                }
            }
        }
    }
}
