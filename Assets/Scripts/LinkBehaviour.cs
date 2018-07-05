using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ODT.GO.Board
{
    public class LinkBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float scaleTime = 0.25f;
        [SerializeField]
        private float delay = 1f;
        [SerializeField]
        private iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;
        [SerializeField]
        private float borderWidth = 0f;

        public void DrawLink(Vector3 startPosition, Vector3 endPosition)
        {
            transform.localScale = new Vector3(2f, 1f, 0f);

            Vector3 direction = endPosition - startPosition;
            float xScale = direction.magnitude;

            Vector3 newScale = new Vector3(2f, 1f, BoardBehaviour.SPACING + borderWidth);

            transform.rotation = Quaternion.LookRotation(direction);

            transform.position = startPosition;

            iTween.ScaleTo(gameObject, iTween.Hash(
                "Time", scaleTime,
                "scale", newScale,
                "easetype", easeType,
                "delay", delay
                ));

        }
    }
}
