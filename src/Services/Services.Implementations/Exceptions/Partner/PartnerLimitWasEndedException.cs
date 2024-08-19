
namespace Services.Implementations.Exceptions.Partner
{
    public class PartnerLimitWasEndedException : Exception
    {
        public PartnerLimitWasEndedException() : base("Лимит для партнера исчерпан") { }
    }
}
