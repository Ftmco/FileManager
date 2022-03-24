namespace File.Entity.File;

public record Directory
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    public Guid ApplicationId { get; set; }

    //Navigation Property
    //Relationships

    public virtual Application.Application Application { get; set; }

    public virtual ICollection<File> Files { get; set; }
}