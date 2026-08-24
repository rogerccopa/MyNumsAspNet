namespace MyNumsApi.Models;

public class Num
{
    public int Number { get; set; }
    public string Note { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.Now;
}
