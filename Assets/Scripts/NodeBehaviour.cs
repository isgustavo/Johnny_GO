using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ODT.GO.Board
{
    public class NodeBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float scaleTime = 0.3f;
        [SerializeField]
        private iTween.EaseType easyType = iTween.EaseType.easeInExpo;
        [SerializeField]
        private float delay = 1f;
        [SerializeField]
        private GameObject targetComponent;
        [SerializeField]
        private GameObject linkPrefab;
        [SerializeField]
        private LayerMask obstacleLayer;

        private bool isInit = false;

        private Vector3 scale;

        private List<NodeBehaviour> nodeNeighbor = new List<NodeBehaviour>();
        private List<NodeBehaviour> linkedNodes = new List<NodeBehaviour>();

        public void InitNode()
        {
            scale = transform.localScale;
            transform.localScale = Vector3.zero;

            if (!isInit)
            {
                ShowNode();
                InitNeighbors();
                isInit = true;
            }
        }

        public void SetTarget(bool value)
        {
            targetComponent.SetActive(value);
        }

        private void ShowNode()
        {
            iTween.ScaleTo(transform.gameObject, iTween.Hash(
                "time", scaleTime,
                "scale", scale,
                "easetype", easyType,
                "delay", delay));
        }

        public void FindNeighbors(NodeBehaviour[] nodes)
        {
            for (int i = 0; i < BoardBehaviour.directions.Length; i++)
            {
                NodeBehaviour neighbor = Array.Find(nodes, n => n.transform.localPosition == transform.localPosition + BoardBehaviour.directions[i]);

                if (neighbor != null)
                {
                    if (!ExistsObstacle(neighbor))
                    {
                        nodeNeighbor.Add(neighbor);
                    } 
                }
            }
        }

        public NodeBehaviour FindNeigborOnDirection(Vector3 direction)
        {
            return nodeNeighbor.Find(n => n.transform.position == transform.position + direction);
        }

        private void InitNeighbors()
        {
            StartCoroutine(InitNeighborsRoutine());
        }

        private IEnumerator InitNeighborsRoutine()
        {
            yield return new WaitForSeconds(delay);

            for (int i = 0; i < nodeNeighbor.Count; i++)
            {
                if (!linkedNodes.Contains(nodeNeighbor[i]))
                {
                    LinkToNode(nodeNeighbor[i]);
                    nodeNeighbor[i].InitNode();
                }
            }
        }

        private void LinkToNode(NodeBehaviour targetNode)
        {
            if (linkPrefab != null)
            {
                GameObject newLink = Instantiate(linkPrefab, transform);
                newLink.GetComponent<LinkBehaviour>().DrawLink(transform.position, targetNode.transform.position);

                if (!linkedNodes.Contains(targetNode))
                {
                    linkedNodes.Add(targetNode);
                }

                if (!targetNode.linkedNodes.Contains(this))
                {
                    targetNode.linkedNodes.Add(this);
                }
                
            }
        }

        private bool ExistsObstacle(NodeBehaviour targetNode)
        {
            Vector3 direction = targetNode.transform.position - transform.position;

            if (Physics.Raycast(transform.position, direction, BoardBehaviour.SPACING, obstacleLayer))
            {
                return true;
            }

            return false;
        }
    }
}
