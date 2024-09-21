using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    static CameraManager _instance;

    public static CameraManager Instance { get => _instance; }

    [SerializeField]
    CinemachineVirtualCamera _starting;

    [SerializeField]
    CinemachineFreeLook _zoomOut;

    [SerializeField]
    Camera _raycastCam;

    public Vector3 CurrentCameraPos { get => _zoomOut.transform.position; }

    public Camera RaycastCam { get => _raycastCam; }

    int _priority = 20;

    Vector3 _touchStart;

    [SerializeField]
    CinemachineBrain _brain;

    [SerializeField]
    float _zoomOutMin = 1;
    [SerializeField]
    float _zoomOutMax = 8;

    float _lastIncrement = -1;

    Vector3 _zoomOutCameraPos;

    [Min(0)]
    [SerializeField]
    float _timeToCountTouch = 0.25f;

    float _timer = 0;

    Vector2 _pointerOld;

    [SerializeField]
    float _senstivityX = 0.5f;

    [SerializeField]
    float _senstivityY = 0.5f;

    [SerializeField]
    bool _invertXAxis = false;

    [SerializeField]
    bool _invertYAxis = false;

    bool _started = false;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _starting.Priority = _priority;
        _zoomOut.Priority = 0;

        _zoomOutCameraPos = _zoomOut.transform.position;

        UpdateRaycastCam();
    }

    // Start is called before the first frame update
    void Start()
    {
        CameraMoveCakeLayer.Instance.m_currentLayer = 0;

        _starting.Priority = 0;
        _zoomOut.Priority = _priority;

        UpdateRaycastCam();
    }

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;          

            Zoom(difference);
        }

        if (Application.isConsolePlatform || Application.isEditor)
        {
            Zoom(-1 * Input.GetAxis("Mouse ScrollWheel"));
        }

        if (CurrentItem.Instance.ItemSelected != null)
        {
            return;
        }

        if (Input.touchCount == 1)
        {
            if (IsOverUI(Input.GetTouch(0).position))
            {
                return;
            }


            if (_timer >= _timeToCountTouch)
            {
                DoDragRotation();
            }
            else
            {
                _timer += Time.deltaTime;

                if (_timer >= _timeToCountTouch)
                {
                    _pointerOld = Input.touches[0].position;
                }
            }
        }
        else
        {
            _timer = 0;
        }
    }

    void DoDragRotation()
    {
        int invertX = _invertXAxis ? -1 : 1;
        int invertY = _invertYAxis ? -1 : 1;

        Vector2 touchDist = Input.touches[0].position - _pointerOld;
        _pointerOld = Input.touches[0].position;

        _zoomOut.m_XAxis.Value += invertX * touchDist.x * _senstivityX * Time.deltaTime;
        _zoomOut.m_YAxis.Value += invertY * touchDist.y * _senstivityY * Time.deltaTime;

        UpdateRaycastCam();
    }

    bool IsOverUI(Vector2 pos)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = pos;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        for (int i = 0; i < results.Count; i++)
        {
            if (LayerMask.NameToLayer("UI") == results[i].gameObject.layer)
            {
                //touching ui
                return true;
            }
        }

        return false;
    }

    void Zoom(float increment)
    {
        increment *= -1;

        if (increment == 0)
        {
            return;
        }

        if (_zoomOut.m_Lens.Orthographic)
        {
            _zoomOut.m_Lens.OrthographicSize = Mathf.Clamp(_zoomOut.m_Lens.OrthographicSize - increment, _zoomOutMin, _zoomOutMax);
        }
        else
        {
            _zoomOut.m_Lens.FieldOfView = Mathf.Clamp(_zoomOut.m_Lens.FieldOfView - increment, _zoomOutMin, _zoomOutMax);
        }

        UpdateRaycastCam();
    }

    public bool UsingZoomOut()
    {
        return true;
    }

    public void WaitAndUpdateRaycastCam(float time)
    {
        if (time < 0)
        {
            return;
        }

        Invoke(nameof(StartRaycastCam), time);
    }

    void StartRaycastCam()
    {
        _started = true;
        UpdateRaycastCam();
    }

    public void UpdateRaycastCam()
    {
        if (_started)
        {
            _brain.ManualUpdate();
        }
        /*
        _raycastCam.transform.position = _zoomOut.transform.position;
        _raycastCam.transform.LookAt(_zoomOut.LookAt);
        */

        _raycastCam.transform.SetPositionAndRotation(Camera.main.transform.position, Camera.main.transform.rotation);

        //_raycastCam.transform.SetPositionAndRotation(_zoomOut.transform.position, _zoomOut.transform.rotation);

        if (_raycastCam.orthographic)
        {
            _raycastCam.orthographicSize = _zoomOut.m_Lens.OrthographicSize;
        }
        else
        {
            _raycastCam.fieldOfView = _zoomOut.m_Lens.FieldOfView;
        }
    }
}
