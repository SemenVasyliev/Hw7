namespace Hw7.Exercise3.Abstractions
{
    /// <summary>
    /// Currency service.
    /// </summary>
    public interface ICurrencyService
    {
        /// <summary>
        /// Exchanges money from source currency into destination currency.
        /// </summary>
        /// <param name="srcCurrency">Source currency ISO code.</param>
        /// <param name="destCurrency">Destination currency ISO code.</param>
        /// <param name="amount">Source amount.</param>
        /// <returns>Returns exchanged amount, or <c>null</c> if one of the currencies is not defined.</returns>
        decimal? Exchange(string srcCurrency, string destCurrency, decimal amount);

        /// <summary>
        /// Tries to get currency rate (relatively to base currency).
        /// </summary>
        /// <param name="currencyCode">Currency ISO code.</param>
        /// <returns>
        /// Returns currency rate (relatively to base currency) 
        /// or <c>null</c> if currency is not defined.
        /// </returns>
        decimal? GetCurrencyRate(string currencyCode);

        /// <summary>
        /// Sets currecy rate. 
        /// </summary>
        /// <param name="currencyCode">Currency ISO code.</param>
        /// <param name="rate">
        /// Currency rate (relatively to base currency).
        /// Should be greater than zero.
        /// </param>
        void SetCurrencyRate(string currencyCode, decimal rate);
    }
}