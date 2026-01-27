public interface IInteractable
{
    // Called when the player presses the Interact key nearby
    void Interact();
    
    // Optional: Return a string to show on UI (e.g., "Press E to Open Chest")
    string GetInteractText();
}