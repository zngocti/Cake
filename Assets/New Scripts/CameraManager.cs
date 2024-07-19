using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static CameraManager _instance;

    public static CameraManager Instance { get => _instance; }

    [SerializeField]
    ZoomButton _zoomOutButton;

    [SerializeField]
    ZoomButton _zoomInButton;

    [SerializeField]
    GameObject _goUpButton;

    [SerializeField]
    GameObject _goDownButton;

    [SerializeField]
    CinemachineVirtualCamera _starting;

    [SerializeField]
    CinemachineVirtualCamera _zoomOut;

    [SerializeField]
    CinemachineVirtualCamera[] _side = new CinemachineVirtualCamera[0];

    [SerializeField]
    CinemachineVirtualCamera[] _top = new CinemachineVirtualCamera[0];

    [SerializeField]
    Camera _raycastCam;

    CinemachineVirtualCamera _currentCam;

    public Vector3 CurrentCameraPos { get => _currentCam.transform.position; }

    public Camera RaycastCam { get => _raycastCam; }

    int _priority = 20;

    bool _usingSide = true;
    int _currentHeight = 0;

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

        for (int i = 0; i < _side.Length; i++)
        {
            _side[i].Priority = 0;
        }

        for (int i = 0; i < _top.Length; i++)
        {
            _top[i].Priority = 0;
        }

        _currentCam = _starting;
        UpdateRaycastCam();
    }

    // Start is called before the first frame update
    void Start()
    {
        CameraMoveCakeLayer.Instance.m_currentLayer = 0;
        ZoomOut(false);
    }

    public void ZoomOut(bool moveButons = true)
    {
        if (_currentCam == _zoomOut)
        {
            return;
        }

        _currentCam.Priority = 0;
        _zoomOut.Priority = _priority;
        _currentCam = _zoomOut;
        UpdateRaycastCam();

        if (moveButons)
        {
            PushZoomOutButton();
        }

        UpdateArrowButtons();
    }

    public void ZoomIn(bool moveButons = true)
    {
        if (_currentCam != _zoomOut)
        {
            return;
        }

        _currentCam.Priority = 0;

        if (_usingSide)
        {
            _currentCam = _side[_currentHeight];
        }
        else
        {
            _currentCam = _top[_currentHeight];
        }

        UpdateRaycastCam();

        _currentCam.Priority = _priority;

        if (moveButons)
        {
            PushZoomInButton();
        }

        UpdateArrowButtons();
    }

    public void GoUp()
    {
        if (_currentCam == _zoomOut)
        {
            return;
        }

        if (_usingSide && _currentHeight >= _side.Length)
        {
            return;
        }
        else if (!_usingSide && _currentHeight >= _top.Length)
        {
            return;
        }

        _currentHeight++;
        CameraMoveCakeLayer.Instance.m_currentLayer++;

        _currentCam.Priority = 0;

        if (_usingSide)
        {
            _currentCam = _side[_currentHeight];
        }
        else
        {
            _currentCam = _top[_currentHeight];
        }

        UpdateRaycastCam();

        _currentCam.Priority = _priority;

        UpdateArrowButtons();
    }

    public void GoDown()
    {
        if (_currentCam == _zoomOut)
        {
            return;
        }

        if (_currentHeight <= 0)
        {
            return;
        }

        _currentHeight--;
        CameraMoveCakeLayer.Instance.m_currentLayer--;

        _currentCam.Priority = 0;

        if (_usingSide)
        {
            _currentCam = _side[_currentHeight];
        }
        else
        {
            _currentCam = _top[_currentHeight];
        }

        UpdateRaycastCam();

        _currentCam.Priority = _priority;

        UpdateArrowButtons();
    }

    void PushZoomOutButton()
    {
        _zoomOutButton.PushButton();
        _zoomInButton.PullButton();
    }

    void PushZoomInButton()
    {
        _zoomOutButton.PullButton();
        _zoomInButton.PushButton();
    }

    void UpdateArrowButtons()
    {
        if (_currentCam == _zoomOut)
        {
            _goUpButton.SetActive(false);
            _goDownButton.SetActive(false);
            return;
        }

        if (_currentHeight <= 0)
        {
            _goUpButton.SetActive(true);
            _goDownButton.SetActive(false);
            return;
        }

        if (_currentHeight >= CameraMoveCakeLayer.Instance.m_maxLayers)
        {
            _goUpButton.SetActive(false);
            _goDownButton.SetActive(true);
            return;
        }

        _goUpButton.SetActive(true);
        _goDownButton.SetActive(true);
    }

    public void UseSide()
    {
        if (_usingSide)
        {
            return;
        }

        _usingSide = true;

        if (_currentCam == _zoomOut)
        {
            return;
        }

        _currentCam.Priority = 0;
        _currentCam = _side[_currentHeight];
        _currentCam.Priority = _priority;
    }

    public void UseTop()
    {
        if (!_usingSide)
        {
            return;
        }

        _usingSide = false;

        if (_currentCam == _zoomOut)
        {
            return;
        }

        _currentCam.Priority = 0;
        _currentCam = _top[_currentHeight];
        _currentCam.Priority = _priority;
    }

    public bool UsingZoomOut()
    {
        return _currentCam == _zoomOut;
    }

    void UpdateRaycastCam()
    {
        _raycastCam.transform.SetPositionAndRotation(_currentCam.transform.position, _currentCam.transform.rotation);
        _raycastCam.fieldOfView = _currentCam.m_Lens.FieldOfView;
    }
}
