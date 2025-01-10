using UnityEngine;

public class TouchHandlerGame : MonoBehaviour
{
    private MovingObject _currentMovingObject;
    [SerializeField] private Transform _parentToMove;

    private bool isTrakingTouch = false;

    private void Start()
    {
        EventsGame.OnStartGame += RunTrakingTouch;
        EventsGame.OnStopGame += StopTrakingTouch;
        EventsGame.OnTrakingTouchActive += ChangeStatusActiveTrakingTouch;
    }

    private void OnDisable()
    {
        EventsGame.OnStartGame -= RunTrakingTouch;
        EventsGame.OnStopGame -= StopTrakingTouch;
        EventsGame.OnTrakingTouchActive -= ChangeStatusActiveTrakingTouch;
    }

    private void ChangeStatusActiveTrakingTouch(bool status)
    {
        if (status)
        {
            RunTrakingTouch();
        }
        else
        {
            StopTrakingTouch();
        }
    }

    private void RunTrakingTouch()
    {
        isTrakingTouch = true;
    }

    private void StopTrakingTouch()
    {
        isTrakingTouch = false;
    }

    private void Update()
    {
        if (isTrakingTouch)
        {
            if ((Input.touchCount > 0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, 1 << 4);

                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:
                        if (hit.collider != null)
                        {
                            _parentToMove.transform.position = mousePos2D;
                            if (hit.collider.TryGetComponent(out MovingObject movObj))
                            {
                                _currentMovingObject = movObj;
                                _currentMovingObject.DownTap();
                                _currentMovingObject.transform.parent = _parentToMove;
                            }
                        }
                        break;
                    case TouchPhase.Moved:
                        if (_currentMovingObject != null)
                        {
                            if (_currentMovingObject.isReadyToMove)
                                _parentToMove.position = mousePos2D;
                        }
                        break;
                    case TouchPhase.Ended:
                        if (_currentMovingObject != null)
                        {
                            _currentMovingObject.UpTap();
                            _currentMovingObject.transform.parent = null;
                            _currentMovingObject.StopMove();
                            _currentMovingObject = null;
                        }
                        break;
                }
            }
        }

#if UNITY_EDITOR
        if (isTrakingTouch)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity, 1 << 4);

                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider != null)
                    {
                        _parentToMove.transform.position = mousePos2D;
                        if (hit.collider.TryGetComponent(out MovingObject movObj))
                        {
                            _currentMovingObject = movObj;
                            _currentMovingObject.DownTap();
                            _currentMovingObject.transform.parent = _parentToMove;
                        }
                    }
                }

                if (_currentMovingObject != null)
                {
                    if (_currentMovingObject.isReadyToMove)
                        _parentToMove.position = mousePos2D;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_currentMovingObject != null)
                {
                    _currentMovingObject.UpTap();
                    _currentMovingObject.transform.parent = null;
                    _currentMovingObject.StopMove();
                    _currentMovingObject = null;
                }
            }
        }
#endif
    }
}
