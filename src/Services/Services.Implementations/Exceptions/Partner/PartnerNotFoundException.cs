
namespace Services.Implementations.Exceptions.Partner
{
    public class PartnerNotFoundException : Exception
    {
        public PartnerNotFoundException() : base("Партнер не найден") { }
    }
}
