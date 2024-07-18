using core.Components;

namespace core.Starships;

public interface IStarshipBuilder
{
	IStarshipBuilder WithName(String name);
	IStarshipBuilder WithEngines(List<Engine> engines);
	IStarshipBuilder WithHull(Hull hull);
	IStarshipBuilder WithThrusters(List<Thruster> thrusters);
	IStarshipBuilder WithWingPair((Wing, Wing) wingPair);
	Starship Build();
}
