using System;

namespace Otus.Teaching.PromoCodeFactory.WebHost.Models.Role
{
    /// <summary>
    /// Модель роли.
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

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
