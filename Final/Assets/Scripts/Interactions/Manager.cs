using UnityEngine;

public class Manager : MonoBehaviour {

    public bool hasFuse;
    public bool fuseBoxFixed;
    // Start is called before the first frame update
    void Start() {
        hasFuse = false;
        fuseBoxFixed = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
}
