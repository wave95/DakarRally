using System.Threading.Tasks;

namespace DakarRally.Persistence.Interfaces
{
    /// <summary>
    /// Running race detector interface.
    /// </summary>
    public interface IRunningRaceDetector
    {
        /// <summary>
        /// Detects if there is a running race.
        /// </summary>
        /// <returns>True if there is a race that is already running, otherwise false.</returns>
        Task<bool> IsAnyRaceRunning();
    }
}

