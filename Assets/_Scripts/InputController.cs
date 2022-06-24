using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{


    InputControlls iController;
    [SerializeField] private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        //body = GetComponent<Rigidbody>();

    }

    private void Awake()
    {
        iController = new InputControlls();
        iController.Enable();
    }
    private void OnEnable()
    {

    }
    // Update is called once per frame
    void Update()
    {



    }

    private void FixedUpdate()
    {
        if (iController != null)
        {
            Vector2 dir = iController.GamePad.Move.ReadValue<Vector2>();
            //body.Translate(iController.GamePad.Move.ReadValue<Vector2>());
            body.AddForce(dir * 100.0f);
            Debug.Log(dir);
        }

    }
}
