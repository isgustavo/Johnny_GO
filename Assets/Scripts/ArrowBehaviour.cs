using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ODT.GO.Util
{
    public class ArrowBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float zDistance = .5f;
        [SerializeField]
        private float time = 1f;
        [SerializeField]
        private iTween.EaseType easeType = iTween.EaseType.easeInOutExpo;

        private void OnEnable()
        {
            iTween.MoveBy(gameObject, iTween.Hash(
                "z", zDistance,
                "looptype", iTween.LoopType.loop,
                "time", time,
                "easetype", easeType
                ));
        }
    }
}
