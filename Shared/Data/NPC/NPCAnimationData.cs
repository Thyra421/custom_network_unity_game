public struct NPCAnimationData
{
    public bool isRunning;

    public static NPCAnimationData Zero => new NPCAnimationData(false);

    public NPCAnimationData(bool isRunning) {
        this.isRunning = isRunning;
    }
}
