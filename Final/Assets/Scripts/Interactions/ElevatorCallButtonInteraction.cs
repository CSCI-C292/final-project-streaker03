using System.Collections;
using UnityEngine;

public class ElevatorCallButtonInteraction : Interactable {

    public Manager manager;
    public Material glow;
    public Color glowColor;
    public Color normalColor;
    public float callTime;
    public Transform leftElevatorDoor;
    public Transform rightElevatorDoor;
    public float openSpeed;
    public float leftMovePos;
    public float rightMovePos;
    
    private AudioSource audioData;
    private bool called;
    private bool here;
    private float step;
    private Vector3 leftMoveTo;
    private Vector3 rightMoveTo;
    
    // Start is called before the first frame update
    void Start() {
        called = false;
        here = false;
        step = 0;
        leftMoveTo = new Vector3(leftElevatorDoor.position.x, leftElevatorDoor.position.y, leftMovePos);
        rightMoveTo = new Vector3(rightElevatorDoor.position.x, rightElevatorDoor.position.y, rightMovePos);
        audioData = GetComponent<AudioSource>();
    }

    void Update() {
        if(here) {
            if(step < 1) {
                step += Time.deltaTime * openSpeed;
                leftElevatorDoor.position = Vector3.Lerp(leftElevatorDoor.position, leftMoveTo, step);
                rightElevatorDoor.position = Vector3.Lerp(rightElevatorDoor.position, rightMoveTo, step);
            }
        }
    }

    public override void StartInteract() {
        if(manager.fuseBoxFixed && !called) {
            called = true;
            glow.SetColor(Shader.PropertyToID("_BaseColor"), glowColor);
            glow.SetColor(Shader.PropertyToID("_EmissiveColor"), glowColor);
            StartCoroutine(calling(callTime));
        }
    }

    private void arrived() {
        glow.SetColor(Shader.PropertyToID("_BaseColor"), normalColor);
        glow.SetColor(Shader.PropertyToID("_EmissiveColor"), normalColor);
        here = true;
        audioData.Play();
    }

    IEnumerator calling(float time) {
        yield return new WaitForSecondsRealtime(time);
        arrived();
    }

    public override void HoldInteract() {
        return;
    }

    public override void StopInteract() {
        return;
    }
}
