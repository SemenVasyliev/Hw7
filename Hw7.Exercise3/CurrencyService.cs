using Hw7.Exercise3.Abstractions;

namespace Hw7.Exercise3
{
    /// <summary>
    /// Implementation of the <see cref="ICurrencyService"/>.
    /// </summary>
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyStorage _storage;
        private readonly ICurrencyNotifications _notifications;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="storage">Currencies storage.</param>
        /// <param name="notifications">Currencies notification service.</param>
        /// <exception cref="ArgumentNullException">
        /// Throws when one of the dependencies is <c>null</c>.
        /// </exception>
        public CurrencyService(ICurrencyStorage storage, ICurrencyNotifications notifications)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
            _notifications = notifications ?? throw new ArgumentNullException(nameof(notifications));
        }

        /// <inheritdoc/>
        public decimal? Exchange(string srcCurrency, string destCurrency, decimal amount)
        {
            if (srcCurrency == null)
                throw new ArgumentNullException(nameof(srcCurrency));
            if (destCurrency == null)
                throw new ArgumentNullException(nameof(destCurrency));
            if (amount < decimal.Zero)
                throw new ArgumentOutOfRangeException(nameof(amount));

            var srcRate = _storage.GetCurrencyRate(srcCurrency.ToUpperInvariant());
            var destRate = _storage.GetCurrencyRate(destCurrency.ToUpperInvariant());

            return srcRate == null || destRate == null
                ? null
                : amount * srcRate / destRate;
        }

        /// <inheritdoc/>
        public decimal? GetCurrencyRate(string currencyCode)
        {
            return currencyCode == null
                ? throw new ArgumentNullException(nameof(currencyCode))
                : _storage.GetCurrencyRate(currencyCode.ToUpperInvariant());
        }

        /// <inheritdoc/>
        public void SetCurrencyRate(string currencyCode, decimal rate)
        {
            if (currencyCode == null)
                throw new ArgumentNullException(nameof(currencyCode));

            if (rate <= decimal.Zero)
                throw new ArgumentOutOfRangeException(nameof(rate));

            currencyCode = currencyCode.ToUpperInvariant();

            _storage.UpsertCurrencyRate(currencyCode, rate);

            // TODO : Uncomment before release
            _notifications.NotifyCurrencyRateChanged(currencyCode, rate);
        }
    }
}
