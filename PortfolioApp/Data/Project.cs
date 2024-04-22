namespace Projects.Data
{
    public class Project
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? FileUrl { get; set; }
        public virtual Guid ProjectCategoryId { get; set; }
        public virtual ProjectCategory? ProjectCategory { get; set; }
        public string? GithubLink { get; set; }
        public virtual IEnumerable<Image>? Images { get; set; }
    }
}
