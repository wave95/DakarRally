using System.Threading.Tasks;

namespace DakarRally.Application.Interfaces
{
    /// <summary>
    /// Raace detector interface.
    /// </summary>
    public interface IRaceDetector
    {
        /// <summary>
        /// Checks if a running aleready exists.
        /// </summary>
        Task<bool> AnyRaceRunning();
    }
}
