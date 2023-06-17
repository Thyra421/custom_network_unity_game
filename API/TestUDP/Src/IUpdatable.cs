using System.Timers;

namespace TestUDP;

public interface IUpdatable
{
    /// <summary>
    /// Starts triggering <see cref="Update"/> <paramref name="frequency"/> times per seconds.
    /// </summary>
    /// <param name="frequency">How many times the Update should trigger each second.</param>
    public void Start(float frequency) {
        System.Timers.Timer timer = new System.Timers.Timer(1000 / frequency);
        timer.Elapsed += (object sender, ElapsedEventArgs e) => { Update(); };
        timer.AutoReset = true;
        timer.Start();
    }

    public void Update();
}
