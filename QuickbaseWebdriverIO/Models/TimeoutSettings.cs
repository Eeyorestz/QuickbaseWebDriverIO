namespace QuickbaseWebdriverIO.Models
{
    public class TimeoutSettings
    {
        /// <summary>
        /// Gets or sets timeout for all base operations. Miliseconds.
        /// </summary>
        public int GeneralTimeout { get; set; }

        /// <summary>
        /// Gets or sets timeout for additional operations. Miliseconds.
        /// </summary>
        public int AdditionalTimeout { get; set; }

        /// <summary>
        /// Gets or sets sleep interval for all operations. Miliseconds.
        /// </summary>
        public int SleepInterval { get; set; }
    }
}
