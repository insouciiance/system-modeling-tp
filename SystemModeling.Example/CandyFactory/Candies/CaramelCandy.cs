namespace SystemModeling.Example.CandyFactory.Candies;

internal class CaramelCandy : ICandy
{
    public CaramelAroma Aroma { get; set; }

    public float GetPrice() => 3;
}
