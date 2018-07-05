using ODT.GO.Enemy;
using ODT.GO.Player;
using ODT.Util.Scriptable;
using System;
using UnityEngine;

namespace ODT.GO.Board
{
    public class BoardBehaviour : MonoBehaviour
    {
        public static float SPACING = 4f;

        public static readonly Vector3[] directions =
        {
            new Vector3(0f, 0f, -SPACING),
            new Vector3(0f, 0f, SPACING),
            new Vector3(SPACING, 0f, 0f),
            new Vector3(-SPACING, 0f, 0f),
        };

        [Header("Board")]
        [SerializeField]
        private NodeBehaviour firstNode;
        public NodeBehaviour FirstNode
        {
            get
            {
                return firstNode;
            }
        }

        [SerializeField]
        private NodeBehaviour targetNode;

        private NodeBehaviour[] nodes;

        private void OnEnable()
        {
            InitNodes();
        }

        private void InitNodes()
        {
            nodes = GetComponentsInChildren<NodeBehaviour>();

            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].FindNeighbors(nodes);

                if (nodes[i] == targetNode)
                {
                    nodes[i].SetTarget(true);
                }
                else
                {
                    nodes[i].SetTarget(false);
                }
            }

            firstNode.InitNode();
        }

        /*public bool IsPlayerOnMySide(NodeBehaviour lookAtNode)
        {
            if (player.CurrentNode.transform.position == lookAtNode.transform.position)
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/
    }

}
