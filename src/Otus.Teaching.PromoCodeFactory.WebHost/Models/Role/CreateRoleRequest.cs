namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.Role
{
    /// <summary>
    /// Модель создания роли.
    /// </summary>
    public class CreateRoleRequest
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }
    }
}
