using ODT.UI.Util;
using UnityEngine;

namespace ODT.UI.Util
{
    public class UISwipeBehaviour : MonoBehaviour
    {
        public static string HORIZONTAL_SWIPE_INPUT = "HorizontalSwipe";
        public static string VERTICAL_SWIPE_INPUT = "VerticalSwipe";

        private Vector3 startTouchPosition, endTouchPosition;

        private void OnEnable()
        {
            UIVirtualInput.AddInput(HORIZONTAL_SWIPE_INPUT);
            UIVirtualInput.AddInput(VERTICAL_SWIPE_INPUT);
        }

        private void Update()
        {
#if UNITY_EDITOR
            OnUnityEditorInput();
#else
            OnTouchInput ();
#endif
        }

        private void OnUnityEditorInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                startTouchPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                endTouchPosition = Input.mousePosition;

                UpdateHorizontalInput();
                UpdateVerticalInput();
            }
            else
            {
                UIVirtualInput.UpdateInput(HORIZONTAL_SWIPE_INPUT, 0);
                UIVirtualInput.UpdateInput(VERTICAL_SWIPE_INPUT, 0);
            }
        }

        private void OnTouchInput ()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startTouchPosition = Input.GetTouch(0).position;
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                endTouchPosition = Input.GetTouch(0).position;

                UpdateHorizontalInput();
                UpdateVerticalInput();
            }
            else
            {
                UIVirtualInput.UpdateInput(HORIZONTAL_SWIPE_INPUT, 0);
                UIVirtualInput.UpdateInput(VERTICAL_SWIPE_INPUT, 0);
            }
        }

        private void UpdateHorizontalInput()
        {
            if (endTouchPosition.x > startTouchPosition.x + 200)
            {
                UIVirtualInput.UpdateInput(HORIZONTAL_SWIPE_INPUT, 1);
            }
            else if (endTouchPosition.x < startTouchPosition.x - 200)
            {
                UIVirtualInput.UpdateInput(HORIZONTAL_SWIPE_INPUT, -1);
            }
            else
            {
                UIVirtualInput.UpdateInput(HORIZONTAL_SWIPE_INPUT, 0);
            }
        }

        private void UpdateVerticalInput()
        {
            if (endTouchPosition.y > startTouchPosition.y + 200)
            {
                UIVirtualInput.UpdateInput(VERTICAL_SWIPE_INPUT, 1);
            }
            else if (endTouchPosition.y < startTouchPosition.y - 200)
            {
                UIVirtualInput.UpdateInput(VERTICAL_SWIPE_INPUT, -1);
            }
            else
            {
                UIVirtualInput.UpdateInput(VERTICAL_SWIPE_INPUT, 0);
            }
        }
    }
}
