using UnityEngine;

namespace ODT.GO.Board
{
    public class TargetBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float scaleTime = 0.25f;
        [SerializeField]
        private float delay = 1f;
        [SerializeField]
        private float rotateSpeed = 20f;

        private void OnEnable()
        {
            iTween.ScaleFrom(gameObject, iTween.Hash(
                "scale", Vector3.zero,
                "time", scaleTime,
                "delay", delay,
                "easetype", iTween.EaseType.linear
                ));

            iTween.RotateBy(gameObject, iTween.Hash(
                "y", 360f,
                "looptype", iTween.LoopType.loop,
                "speed", rotateSpeed,
                "easetype", iTween.EaseType.linear
                ));
        }
    }
}
