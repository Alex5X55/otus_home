using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Otus.Teaching.PromoCodeFactory.WebHost.PromoCodeStatusCodeResults
{
    /// <summary>
    /// A <see cref="StatusCodeResult"/> that when
    /// executed will produce a Bad Request (400) response.
    /// </summary>
    [DefaultStatusCode(DefaultStatusCode)]
    public class PartnerLimitIsEmptyObjectResult : ObjectResult
    {
        private const int DefaultStatusCode = 715;

        /// Creates a new <see cref="NotFoundObjectResult"/> instance.
        /// </summary>
        /// <param name="value">The value to format in the entity body.</param>
        public PartnerLimitIsEmptyObjectResult([ActionResultObjectValue] object? value)
            : base(value)
        {
            StatusCode = DefaultStatusCode;
        }
    }
}
