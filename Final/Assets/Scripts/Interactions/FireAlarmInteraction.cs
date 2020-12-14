using UnityEngine;

public class FireAlarmInteraction : Interactable {

    public Animator animator;

    private bool pulled;

    void Start() {
        pulled = false;
    }

    public override void StartInteract() {
        if(!pulled) {
            animator.Play("Pull Lever");
            pulled = true;
        }
    }

    public override void HoldInteract() {
        return;
    }

    public override void StopInteract() {
        return;
    }
}
