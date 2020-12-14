using UnityEngine;

public class PlayerLook : MonoBehaviour {

    public Camera cam;
    public float lookSpeedX;
    public float lookSpeedY;
    public float upperYClamp;
    public float lowerYClamp;

    private float currentY;
    
    // Start is called before the first frame update
    void Start() {
        currentY = 0f;
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(Vector3.up, Input.GetAxisRaw("Mouse X") * lookSpeedX * Time.deltaTime);
        currentY -= Input.GetAxisRaw("Mouse Y") * lookSpeedY * Time.deltaTime;
        currentY = Mathf.Clamp(currentY, upperYClamp * -1, lowerYClamp);
        cam.transform.localRotation = Quaternion.Euler(currentY, 0f, 0f);


    }
}
