using System.ComponentModel.DataAnnotations;

namespace Presentation.Settings
{
    public class DatabaseSettings
    {
        [Required]
        public string ConnectionString { get; set; } = string.Empty;
    }
}
