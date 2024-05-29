using core.Components;

namespace core.Starships;

public interface IStarshipBuilder
{
	IStarshipBuilder WithEngine(Engine engine);
	IStarshipBuilder WithHull(Hull hull);
	IStarshipBuilder WithThrusters(List<Thruster> thrusters);
	IStarshipBuilder WithWing(Wing wing);
	Starship Build();
}
