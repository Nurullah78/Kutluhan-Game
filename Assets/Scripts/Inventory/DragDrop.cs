using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {

//---------------Nesneler---------------

        [SerializeField] Canvas canvas;
        
        public static GameObject ItemBeingDragged;
        
        RectTransform _rectTransform;
        CanvasGroup _canvasGroup;

        Vector3 _startPosition;
        Transform _startParent;


//---------------Metotlar---------------

        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            _canvasGroup.alpha = .6f;

            //So the ray cast will ignore the item itself.
            _canvasGroup.blocksRaycasts = false;
            _startPosition = transform.position;
            transform.SetParent(transform.root);
            ItemBeingDragged = gameObject;
        }

        public void OnDrag(PointerEventData eventData)
        {
            //So the item will move with our mouse (at same speed) and so it will be consistent if the canvas has a different scale (other then 1).
            _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ItemBeingDragged = null;

            if (transform.parent == _startParent || transform.parent == transform.root)
            {
                transform.position = _startPosition;
                transform.SetParent(_startParent);
            }

            Debug.Log("OnEndDrag");
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}