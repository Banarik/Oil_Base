using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraSpeed;
    public float rotateSpeed;
    public float scaleSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float height = transform.position.y;
        Vector2 MousePosition = Input.mousePosition;
        if (MousePosition.x >= Screen.width - 10)
        {
            transform.Rotate(0, rotateSpeed, 0, Space.World)  ;
        }
        if (MousePosition.x <= 10)
        {
            transform.Rotate(0, -rotateSpeed,0,Space.World)  ;
        }
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * cameraSpeed);
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * cameraSpeed);
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * scaleSpeed);
        }
        
    }
}
