using ODT.GO.Board;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace ODT.GO.Util
{
    [Serializable]
    public class OnNodeChanged : UnityEvent<NodeBehaviour> { }

    public abstract class MoveBehaviour : MonoBehaviour
    {
        [SerializeField]
        protected float moveSpeed = 5.5f;
        [SerializeField]
        protected float delay = 0f;
        [SerializeField]
        protected iTween.EaseType easyType = iTween.EaseType.easeOutSine;

        protected bool isMoving = false;

        protected NodeBehaviour currentNode;
        public NodeBehaviour CurrentNode
        {
            get
            {
                return currentNode;
            } 
        }

        [SerializeField]
        protected UnityEvent OnMoveEvent;

        [SerializeField]
        protected OnNodeChanged OnNodeChanged;

        protected void MoveTo(NodeBehaviour destinationNode, float delayTime = .25f)
        {
            iTween.MoveTo(gameObject, iTween.Hash(
                "x", destinationNode.transform.position.x,
                "z", destinationNode.transform.position.z,
                "delay", delayTime,
                "easetype", easyType,
                "speed", moveSpeed,
                "oncomplete", "OnMoveComplete",
                "oncompletetarget", gameObject,
                "oncompleteparams", destinationNode
                ));

            OnMoveEvent.Invoke();
            isMoving = true;
        }

        protected void OnMoveComplete(NodeBehaviour destinationNode)
        {
            isMoving = false;
            SetCurrentNode(destinationNode);
        }

        protected void MoveLeft()
        {
            if (isMoving)
            {
                return;
            }

            NodeBehaviour newNode = currentNode.FindNeigborOnDirection(new Vector3(-BoardBehaviour.SPACING, 0, 0));
            if (newNode != null)
            {
                MoveTo(newNode, delay);
            }
        }

        protected void MoveRight()
        {
            if (isMoving)
            {
                return;

            }
            NodeBehaviour newNode = currentNode.FindNeigborOnDirection(new Vector3(BoardBehaviour.SPACING, 0, 0));
            if (newNode != null)
            {
                MoveTo(newNode, delay);
            }
        }

        protected void MoveForward()
        {
            if (isMoving)
            {
                return;
            }

            NodeBehaviour newNode = currentNode.FindNeigborOnDirection(new Vector3(0, 0, BoardBehaviour.SPACING));
            if (newNode != null)
            {
                MoveTo(newNode, delay);
            }
        }

        protected void MoveBackward()
        {
            if (isMoving)
            {
                return;
            }

            NodeBehaviour newNode = currentNode.FindNeigborOnDirection(new Vector3(0, 0, -BoardBehaviour.SPACING));
            if (newNode != null)
            {
                MoveTo(newNode, delay);
            }
        }

        public void SetCurrentNode(NodeBehaviour node)
        {
            currentNode = node;
            OnNodeChanged.Invoke(currentNode);
        }
    }
}
