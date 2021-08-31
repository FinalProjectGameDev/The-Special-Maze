using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelChairController : MonoBehaviour
{
    public Animator anim;
    private CharacterController _cc;
    Vector3 velocity;
    [SerializeField] float _speed = 3.5f;
    [SerializeField] private float _speedRotation = 0.5f;

    [HideInInspector] public vThirdPersonCamera tpCamera;
    [HideInInspector] public Camera cameraMain;


    // Start is called before the first frame update
    void Start()
    {
        _cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float side = 0;
        float move = 0;
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            side = 0;
            move = 1;
            _cc.Move(transform.forward * _speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            side = 0.5f;
            move = 0.5f;

        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            side = -0.5f;
            move = 0.5f;
        }
        anim.SetFloat("turn", side);
        anim.SetFloat("move", move);
        Vector3 rotation = transform.localEulerAngles;
        rotation.y += side * _speedRotation;
        transform.localEulerAngles = rotation;
    }

    protected virtual void InitializeTpCamera()
    {
        if (tpCamera == null)
        {
            tpCamera = FindObjectOfType<vThirdPersonCamera>();
            if (tpCamera == null)
                return;
            if (tpCamera)
            {
                tpCamera.SetMainTarget(this.transform);
                tpCamera.Init();
            }
        }
    }

    protected virtual void CameraInput()
    {
        if (!cameraMain)
        {
            if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
            else
            {
                cameraMain = Camera.main;
                //cc.rotateTarget = cameraMain.transform;
            }
        }

        if (cameraMain)
        {
            //cc.UpdateMoveDirection(cameraMain.transform);
        }

        if (tpCamera == null)
            return;

        var Y = Input.GetAxis("Mouse X");
        var X = Input.GetAxis("Mouse Y");

        tpCamera.RotateCamera(X, Y);
    }
}
