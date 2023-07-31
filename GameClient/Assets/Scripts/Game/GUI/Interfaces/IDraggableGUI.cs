using UnityEngine;

public interface IDraggableGUI
{
    public bool IsReadyToBeDragged { get; }
    public Sprite DragIndicator { get; }

    public void Discard();
}