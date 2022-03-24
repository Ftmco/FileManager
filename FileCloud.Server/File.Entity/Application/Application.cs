namespace File.Entity.Application;

public record Application
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string ApiKey { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public Guid OwnerId { get; set; }

    //Navigation Property
    //Relationships

    public virtual ICollection<File.Directory> Directories { get; set; }
}