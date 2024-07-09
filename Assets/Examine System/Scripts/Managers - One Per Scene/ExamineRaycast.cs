using UnityEngine;
using UnityEngine.UI;

namespace ExamineSystem
{
    [RequireComponent(typeof(Camera))]
    public class ExamineRaycast : MonoBehaviour
    {
        [Header("Raycast Features")]
        [SerializeField] private float rayLength = 5;
        private ExamineItemController examinableItem;
        private Camera _camera;

        [Header("Crosshair")]
        [SerializeField] private Image uiCrosshair = null;

        public bool IsLookingAtExaminable
        {
            get { return examinableItem != null; }
        }

        void Start()
        {
            _camera = GetComponent<Camera>();
        }

        void Update()
        {
            if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out RaycastHit hit, rayLength))
            {
                Debug.Log("hit" + hit.transform.name);
                var examineItem = hit.collider.GetComponent<ExamineItemController>();
                if (examineItem != null)
                {
                    examinableItem = examineItem;
                    examinableItem.MainHighlight(true);


                    Debug.Log("examine Item " + examinableItem);
                }
                else
                {
                    Debug.Log("not working");
                    ClearExaminable();
                }
            }
            else
            {
                ClearExaminable();
            }

            if (IsLookingAtExaminable)
            {
                if (Input.GetKeyDown(ExamineInputManager.instance.interactKey))
                {
                    examinableItem.ExamineObject();
                }
            }
        }

        private void ClearExaminable()
        {
            if (examinableItem != null)
            {
                examinableItem.MainHighlight(false);

                examinableItem = null;
            }
        }

    }
}
