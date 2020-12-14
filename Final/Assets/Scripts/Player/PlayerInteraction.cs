using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    public Camera cam;
    public float range;
    public LayerMask interactableLayer;

    private Interactable current;
    private bool interacting;
    // Start is called before the first frame update
    void Start() {
        interacting = false;
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.F)) {
            RaycastHit hit;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Debug.Log("Fired");
            if(Physics.Raycast(ray, out hit, range, ~interactableLayer)) {
                Debug.Log(hit.transform.gameObject.name);
                current = hit.transform.gameObject.GetComponent<Interactable>();
                current.StartInteract();
                interacting = true;
            }
        }

        if(Input.GetKey(KeyCode.F)) {
            if(interacting) {
                current.HoldInteract();
            }
        }

        if(Input.GetKeyUp(KeyCode.F)) {
            if(interacting) {
                current.StopInteract();
            }
        }

        if(interacting) {
            RaycastHit hit;
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if(Physics.Raycast(ray, out hit, range, ~interactableLayer)) {
                return;
            } else {
                interacting = false;
                current.StopInteract();
            }
        }

    }
}
