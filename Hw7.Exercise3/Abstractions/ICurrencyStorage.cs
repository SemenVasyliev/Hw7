namespace Hw7.Exercise3.Abstractions
{
    /// <summary>
    /// Curency rates persistent storage.
    /// </summary>
    public interface ICurrencyStorage
    {
        /// <summary>
        /// Tries to get currency rate from storage.
        /// </summary>
        /// <param name="currencyCode">Currency ISO code.</param>
        /// <returns>
        /// Returns currency rate (relatively to base currency) 
        /// or <c>null</c> if currency is not defined.
        /// </returns>
        decimal? GetCurrencyRate(string currencyCode);

        /// <summary>
        /// Update or insert new currency rate to storage.
        /// </summary>
        /// <param name="currencyCode">Currency ISO code.</param>
        /// <param name="rate">Currency rate.</param>
        void UpsertCurrencyRate(string currencyCode, decimal rate);
    }
}
