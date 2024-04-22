using System.ComponentModel.DataAnnotations;

namespace Projects.Data
{
    public class Image
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string? FileUrl { get; set; }
        [Required]
        public Guid ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
