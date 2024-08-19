
namespace Services.Implementations.Exceptions.Partner
{
    public class PartnerLimitIsEmptyException : Exception
    {
        public PartnerLimitIsEmptyException() : base("Лимит для партнера должен быть больше 0") { }
    }
}
