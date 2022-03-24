namespace File.Entity.File;

public record File
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid DirectoryId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string OriginalName { get; set; }

    [Required]
    public string Extension { get; set; }

    [Required]
    public double Size { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    //Navigation Property
    //Relationships

    public virtual Directory Directory { get; set; }
}