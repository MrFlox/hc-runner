using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class RunnerInputController : MonoBehaviour
{

    InputControlls controlls;

    public float minX, maxX;
    [SerializeField]
    private float koeff = 1;
    [SerializeField] private GameObject left, right, center;

    Vector2 touchStart = Vector3.zero;

    Vector3 centerStartPosition = Vector3.zero;

    public Vector3 targetPoint = Vector3.zero;

    public static RunnerInputController instance;

    public bool touch = false;

    void Awake() => instance = this;
    bool pointerOverUI = false;
    void Start()
    {
        minX = left.transform.position.x;
        maxX = right.transform.position.x;
        //---------------------------------------------
        controlls = new InputControlls();
        controlls.Enable();
        controlls.GamePad.TouchStart.performed += OnTouchStart;
        controlls.GamePad.TouchStart.canceled += onTouchCancel;
    }

    private void onTouchCancel(InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.State != GameState.GamePlay) return;
        touch = false;
    }

    private void OnTouchStart(InputAction.CallbackContext obj)
    {
        if (GameManager.Instance.State != GameState.GamePlay) return;
        touch = true;
        centerStartPosition = new Vector3(center.transform.position.x, center.transform.position.y, center.transform.position.z);
        touchStart = controlls.GamePad.TouchMove.ReadValue<Vector2>();
    }

    void OnDisable()
    {
        if (controlls == null) return;
        controlls.Disable();
        controlls.GamePad.TouchStart.performed -= OnTouchStart;
        controlls.GamePad.TouchStart.canceled -= onTouchCancel;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawRay(new Ray(center.transform.position, (center.transform.position - transform.position)));
    }




    void Update()
    {
        pointerOverUI = EventSystem.current.IsPointerOverGameObject();

        if (pointerOverUI) return;

        if (GameManager.Instance.State != GameState.GamePlay) return;
        Vector2 curPos = controlls.GamePad.TouchMove.ReadValue<Vector2>();
        float diff = (curPos.x - touchStart.x);
        Vector3 newPos = new Vector3(centerStartPosition.x + diff * koeff, center.transform.position.y, center.transform.position.z);

        if (newPos.x > maxX) newPos.x = maxX;
        if (newPos.x < minX) newPos.x = minX;
        center.transform.position = newPos;
        targetPoint = center.transform.position;
    }

}
