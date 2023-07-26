public struct NPCAnimationData
{
    public bool isRunning;

    public NPCAnimationData(bool isRunning) {
        this.isRunning = isRunning;
    }

    public static NPCAnimationData Zero => new NPCAnimationData(false);
}
