
namespace Services.Implementations.Exceptions.Partner
{
    public class PartnerIsBlokedException : Exception
    {
        public PartnerIsBlokedException() : base("Партнер заблокирован") { }
    }
}
