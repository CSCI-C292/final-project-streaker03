
public class FuseInteraction : Interactable {

	public Manager manager;

	public override void StartInteract() {
		manager.hasFuse = true;
		Destroy(gameObject);
	}

	public override void HoldInteract() {
		
	}

	public override void StopInteract() {
		
	}
}
