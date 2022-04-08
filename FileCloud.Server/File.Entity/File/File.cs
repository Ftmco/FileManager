namespace File.Entity;

public record DirectoryFile
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string FileName { get; set; }

    [Required]
    public string Mime { get; set; }

    [Required]
    public string Eextension { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    [Required]
    public Guid DirectoryId { get; set; }

    [Required]
    public Guid OwnerId { get; set; }

    [Required]
    public string Token { get; set; }


    //Navigation Property
    //Relationships

    public virtual FDirectory Directory { get; set; }
}