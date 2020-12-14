using UnityEngine;

public abstract class Interactable : MonoBehaviour {

    public virtual void StartInteract() { }

    public virtual void HoldInteract() { }

    public virtual void StopInteract() { }
}
