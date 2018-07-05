using ODT.GO.Board;
using ODT.GO.Util;
using UnityEngine;

namespace ODT.GO.Player
{
    public class PlayerCompassBehaviour : CompassBehaviour
    {

        public override void ShowArrowsFrom(NodeBehaviour node)
        {
            for (int i = 0; i < BoardBehaviour.directions.Length; i++)
            {
                NodeBehaviour neighbor = node.FindNeigborOnDirection(BoardBehaviour.directions[i]);

                if (neighbor != null)
                {
                    arrows[i].gameObject.SetActive(true);
                } else
                {
                    arrows[i].gameObject.SetActive(false);
                }
            }
        }

        public override void RemoveArrows()
        {
            for (int i = 0; i < arrows.Length; i++)
            {
                arrows[i].gameObject.SetActive(false);
            }
        }
    }
}
