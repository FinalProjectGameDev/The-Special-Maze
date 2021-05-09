using UnityEngine;

/**
 * This component rotates its object according to the mouse movement in the Y axis, in a given rotation speed.
 */
public class LookY : MonoBehaviour { 
    [SerializeField] private float _speedRotation = 0.5f;

    void Update() {
        float _mouseY = Input.GetAxis("Mouse Y");
        Debug.Log("mouse y = " + _mouseY);
        Vector3 rotation = transform.localEulerAngles;
        rotation.x -= _mouseY * _speedRotation;
        if (rotation.x > 60 && rotation.x < 180) rotation.x = 60;
        if (rotation.x < 300 && rotation.x > 180) rotation.x = 300;
        transform.localEulerAngles = rotation;
    }

    public void changeSensitivity(float sensitivity)
    {
        _speedRotation = sensitivity;
    }
}
