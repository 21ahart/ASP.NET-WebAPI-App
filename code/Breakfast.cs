namespace UniWebApi.Models;

public class BreakfastFood
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Calories { get; set; }
    public bool IsHighProtein { get; set; }
    public string FavoritePairing { get; set; } = string.Empty;
}
