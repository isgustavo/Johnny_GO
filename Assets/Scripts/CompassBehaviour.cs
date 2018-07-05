using ODT.GO.Board;
using UnityEngine;

namespace ODT.GO.Util
{
    public abstract class CompassBehaviour : MonoBehaviour
    {
        protected ArrowBehaviour[] arrows;

        private void OnEnable()
        {
            arrows = GetComponentsInChildren<ArrowBehaviour>();
        }

        public abstract void RemoveArrows();

        public abstract void ShowArrowsFrom(NodeBehaviour node); 
    }
}
