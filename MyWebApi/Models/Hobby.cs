namespace MyWebApi.Models;

public class Hobby
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int YearsPracticed { get; set; }
    public string SkillLevel { get; set; } = "Beginner"; // Beginner, Intermediate, Advanced
    public string Notes { get; set; } = string.Empty;
}
