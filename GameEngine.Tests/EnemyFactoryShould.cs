using System;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class EnemyFactoryShould
    {
        private readonly ITestOutputHelper _output;
        public EnemyFactoryShould(ITestOutputHelper outputHelper)
        {
            _output = outputHelper;
        }
        [Fact(Skip = "This is thhe reason why we are skipping this")]   
        [Trait("Category", "Enenmy")]
        public void CreateNormalEnemyByDefault()
        {
            _output.WriteLine("This is how we add output message");
            EnemyFactory sut = new EnemyFactory();
            Enemy enemy = sut.Create("Zoomvie");
            Assert.IsType<NormalEnemy>(enemy);
        }
        [Fact]
        public void CreateNormalEnemyByDefault_IsNotType()
        {
            EnemyFactory sut = new EnemyFactory();
            Enemy enemy = sut.Create("Zoomvie");
            Assert.IsNotType<DateTime>(enemy);
        }
        [Fact]
        public void CreateBossEnemy()
        {
            EnemyFactory sut = new EnemyFactory();
            Enemy enemy = sut.Create("King", true);
            Assert.IsType<BossEnemy>(enemy);
        }
        [Fact]
        public void CreateBossEnemy_CastReturnType()
        {
            EnemyFactory sut = new EnemyFactory();
            Enemy enemy = sut.Create("King", true);
            //if passes you will gett he casted object
            BossEnemy bossEnemy = Assert.IsType<BossEnemy>(enemy);
            Assert.Equal("King", bossEnemy.Name);
        }

        [Fact]
        public void CreateBossEnemy_AssertAssignabletype()
        {
            EnemyFactory sut = new EnemyFactory();
            Enemy enemy = sut.Create("King", true);
            Assert.IsAssignableFrom<Enemy>(enemy);
        }

        [Fact]
        public void CreateSeperateInstance()
        {
            EnemyFactory sut = new EnemyFactory();
            Enemy enemy1 = sut.Create("Zombie");
            Enemy enemy2 = sut.Create("Zombie");
            Assert.NotSame(enemy1, enemy2);
        }
        [Fact]
        public void NotAllowNullName()
        {
            EnemyFactory sut = new EnemyFactory();
            Assert.Throws<ArgumentNullException>(() => sut.Create(null));
        }
        [Fact]
        public void NotAllowInvalidEnenmyName()
        {
            EnemyFactory sut = new EnemyFactory();
            EnemyCreationException ex =  Assert.Throws<EnemyCreationException>(() => sut.Create("Zoombie", true));
        }

        

    }
}
