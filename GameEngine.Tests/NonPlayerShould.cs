using Xunit;

namespace GameEngine.Tests
{
    public class NonPlayerCharecterShould
    {
        [Theory]
        [MemberData(nameof(InternalHealthDamageTestData.TestData), MemberType = typeof(InternalHealthDamageTestData))]
        public void TakeDamange(int damage, int expectedHealth)
        {
            PlayerCharacter player = new PlayerCharacter();
            player.TakeDamage(damage);
            Assert.Equal(expectedHealth, player.Health);
        }
         
    }
}
