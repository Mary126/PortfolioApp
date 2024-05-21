using System.ComponentModel.DataAnnotations;

namespace Projects.Data
{
    public class Project
    {
        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? FileUrl { get; set; }
        [Required]
        public virtual Guid ProjectCategoryId { get; set; }
        public virtual ProjectCategory? ProjectCategory { get; set; }
        public string? GithubLink { get; set; }
        [Required]
        public virtual IEnumerable<Image>? Images { get; set; }
    }
}
