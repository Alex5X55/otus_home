namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.Emploee
{
    public class CreateEmployeeRequest
    {
        /// <summary>
        /// Фамилия.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string Email { get; set; }
    }
}
