
namespace Services.Implementations.Exceptions.Partner
{
    public class PartnerLimitIsNotSetException : Exception
    {
        public PartnerLimitIsNotSetException() : base("Лимит для партнера не установлен") { }
    }
}
