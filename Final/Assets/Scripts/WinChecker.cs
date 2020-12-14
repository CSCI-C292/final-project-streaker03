using UnityEngine;

public class WinChecker : MonoBehaviour {

    public GameObject winnerScreen;
    public GameObject terror;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.name == "Crouch Check") {
            Destroy(terror);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            winnerScreen.SetActive(true);
        }
    }
}
