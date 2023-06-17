using System.Timers;

public interface IUpdatable
{
    /// <summary>
    /// Starts triggering <see cref="Update"/> <paramref name="frequency"/> times per seconds.
    /// </summary>
    /// <param name="frequency">How many times the Update should trigger each second.</param>
    public void Start(float frequency) {
        Timer timer = new Timer(1000 / frequency);
        timer.Elapsed += (object sender, ElapsedEventArgs e) => { Update(); };
        timer.AutoReset = true;
        timer.Start();
    }

    public void Update();
}
