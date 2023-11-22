namespace SystemModeling.Example.CandyFactory.Candies;

public class CaramelCandy : ICandy
{
    public CaramelAroma Aroma { get; set; }

    public float GetPrice() => 3;
}
