using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class GameStateShould
        : IClassFixture<GameStateFixture>
    {
        private readonly GameStateFixture _gameStateFixture;
        private readonly ITestOutputHelper _output;
        public GameStateShould(GameStateFixture gameStateFixture, 
            ITestOutputHelper testOutput)
        {
            _gameStateFixture = gameStateFixture;
            _output = testOutput;
        }

        [Fact]
        public void DamageAllPlayersWhenEarthquake()
        {
            _output.WriteLine($"GameStateID= {_gameStateFixture.GameState.Id}");
            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            _gameStateFixture.GameState.Players.Add(player1);
            _gameStateFixture.GameState.Players.Add(player2);

            var expectedHealthAfterEarthquake = player1.Health - GameState.EarthquakeDamage;
            _gameStateFixture.GameState.Earthquake();

            Assert.Equal(expectedHealthAfterEarthquake, player1.Health);
            Assert.Equal(expectedHealthAfterEarthquake, player2.Health);

        }
        [Fact]
        public void Reset()
        {
            _output.WriteLine($"GameStateID= {_gameStateFixture.GameState.Id}");
            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            _gameStateFixture.GameState.Players.Add(player1);
            _gameStateFixture.GameState.Players.Add(player2);

            _gameStateFixture.GameState.Reset();

            Assert.Empty(_gameStateFixture.GameState.Players);
        }
    }
}
