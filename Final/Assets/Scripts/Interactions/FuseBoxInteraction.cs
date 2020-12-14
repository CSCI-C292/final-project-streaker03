using UnityEngine;

public class FuseBoxInteraction : Interactable {

    public Manager manager;
    public GameObject fuse;
    
    public override void StartInteract() {
        if(manager.hasFuse && !manager.fuseBoxFixed) {
            manager.fuseBoxFixed = true;
            fuse.SetActive(true);
        }
    }

    public override void HoldInteract() {
        return;
    }

    public override void StopInteract() {
        return;
    }
}
