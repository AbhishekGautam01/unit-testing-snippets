using Xunit;

namespace GameEngine.Tests
{
    [CollectionDefinition("GameState Collection")]
    public class GameStateCollection: ICollectionFixture<GameStateFixture>
    {
    }
}
