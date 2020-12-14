using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class TerrorController : MonoBehaviour {

    public float radius;
    public GameObject player;
    public GameObject gameOverMenu;
    public float audioDist;
    public float maxVolume;
    
    private PlayerMovement pm;
    private NavMeshAgent agent;
    private bool reachedDestination;
    private AudioSource buzz;
    private Light current;
    
    // Start is called before the first frame update
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        reachedDestination = true;
        pm = player.GetComponent<PlayerMovement>();
        buzz = GetComponent<AudioSource>();
        buzz.volume = 0;
        current = null;
    }

    // Update is called once per frame
    void Update() {
        if(reachedDestination) {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;
            NavMeshHit hit;
            if(NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
                agent.SetDestination(hit.position);
                reachedDestination = false;
            }
        }

        if(agent.remainingDistance < 0.1f) {
            reachedDestination = true;
        }

        RaycastHit hit2;
        if(Physics.Linecast(transform.position, player.transform.position, out hit2)) {
            if(hit2.collider.gameObject.name == "Crouch Check") {
                if(pm.hiding == false) {
                    agent.SetDestination(player.transform.position);
                }
            }
        }

        float dist = Vector3.Distance(transform.position, player.transform.position);
        if(dist <= audioDist) {
            buzz.volume = maxVolume - (Vector3.Distance(transform.position, player.transform.position) / audioDist);
        }
        else {
            buzz.volume = 0;
        }

        if(dist <= 1f && pm.hiding == false) {
            gameOverMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(gameObject);
        }

        Collider[] lights = Physics.OverlapSphere(transform.position, 100, 1<<13);
        GameObject min = null;
        float distance = Mathf.Infinity;
        foreach(Collider col in lights) {
            Debug.Log(col.gameObject.name);
            if(Vector3.Distance(transform.position, col.gameObject.transform.position) < distance) {
                min = col.gameObject;
                distance = Vector3.Distance(transform.position, col.transform.position);
            }
        }

        Light light = min.GetComponent<Light>();
        if(light != current) {
            if(current != null) {
                current.enabled = true;
            }
            light.enabled = false;
            current = light;
        }
    }
}
