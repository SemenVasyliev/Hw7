namespace Hw7.Exercise3.Abstractions
{
    /// <summary>
    /// Currency notifications service.
    /// </summary>
    public interface ICurrencyNotifications
    {
        /// <summary>
        /// Notifies subscribers about new currency rate.
        /// </summary>
        /// <param name="currencyCode">Currency ISO code.</param>
        /// <param name="rate">Currency rate.</param>
        void NotifyCurrencyRateChanged(string currencyCode, decimal rate);
    }
}
