namespace File.Entity;

public record FDirectory
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    [Required]
    public DateTime LastUpdateDate { get; set; }

    [Required]
    public Guid OwnerId { get; set; }

    [Required]
    public string Token { get; set; }

    [Required]
    public bool IsActive { get; set; }

    //Navigation Property
    //Relationships
    public virtual ICollection<DirectoryFile> Files { get; set; }
}