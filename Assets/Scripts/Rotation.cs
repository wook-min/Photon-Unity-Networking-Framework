using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float axis;


    public void RotateY(GameObject go)
    {
        axis += Input.GetAxisRaw("Mouse X") * speed * Time.deltaTime;

        // go.transform.Rotate(Vector3.up * axis);

        transform.eulerAngles = new Vector3(0, axis, 0);
    }

    public void RotateX(float minAngle, float maxAngle)
    {
        axis -= Input.GetAxisRaw("Mouse Y") * speed * Time.deltaTime;

        axis = Mathf.Clamp(axis, minAngle, maxAngle);

        transform.localEulerAngles = new Vector3(axis, 0, 0);
    }
}
