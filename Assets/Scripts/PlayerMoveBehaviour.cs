using ODT.GO.Board;
using ODT.GO.Util;
using ODT.UI.Util;
using UnityEngine;

namespace ODT.GO.Player
{
    public class PlayerMoveBehaviour : MoveBehaviour
    {
        private void Update()
        {
            if (LevelManager.instancie.CurrentTurn == Turn.Player)
            {
                int horizontalInput = (int)UIVirtualInput.GetInput(UISwipeBehaviour.HORIZONTAL_SWIPE_INPUT);
                int verticalInput = (int)UIVirtualInput.GetInput(UISwipeBehaviour.VERTICAL_SWIPE_INPUT);

                if (verticalInput == 0)
                {
                    if (horizontalInput < 0)
                    {
                        MoveLeft();
                    }
                    else if (horizontalInput > 0)
                    {
                        MoveRight();
                    }
                }
                else if (horizontalInput == 0)
                {
                    if (verticalInput < 0)
                    {
                        MoveBackward();
                    }
                    else if (verticalInput > 0)
                    {
                        MoveForward();
                    }
                }
            }
        }
    }
}

