using System;
using System.Collections.Generic;
using Xunit;

namespace GameEngine.Tests
{
    public class PlayerCharecterShould : IDisposable
    {
        private readonly PlayerCharacter _sut;
        public PlayerCharecterShould()
        {
            _sut = new PlayerCharacter();
        }
        
        [Fact]
        public void BeInexperienceWhenNew()
        {
            //sut - system under test. 
            Assert.True(_sut.IsNoob);
        }

        [Fact]
        public void CalculateFullName()
        {
            _sut.FirstName = "Abhishek";
            _sut.LastName = "Gautam";
            string expectedValue = "Abhishek Gautam";
            Assert.Equal(expectedValue, _sut.FullName);
        }
        [Fact]
        public void HaveFullNameStartsWIthFirstName()
        {
            _sut.FirstName = "Abhishek";
            _sut.LastName = "Gautam";
            string expectedValue = "Abhishek";
            Assert.StartsWith(expectedValue, _sut.FullName);
        }
        [Fact]
        public void HaveFullNameEndssWIthLastName()
        {
            _sut.FirstName = "Abhishek";
            _sut.LastName = "Gautam";
            string expectedValue = "Gautam";
            Assert.EndsWith(expectedValue, _sut.FullName);
        }

        [Fact]
        public void CalculateFullName_IgnoreCase()
        {
            _sut.FirstName = "Abhishek";
            _sut.LastName = "Gautam";
            string expectedValue = "abhishek gautam";
            Assert.Equal(expectedValue, _sut.FullName, ignoreCase: true);
        }

        [Fact]
        public void CalculateFullName_SubstringAssert()
        {
            _sut.FirstName = "Abhishek";
            _sut.LastName = "Gautam";
            string expectedValue = "ek Ga";
            Assert.Contains(expectedValue, _sut.FullName);
        }

        [Fact]
        public void CalculateFullNameWithTitleCase()
        {
            _sut.FirstName = "Abhishek";
            _sut.LastName = "Gautam";
            Assert.Matches("[A-z]{1}[a-z]+ [A-Z]{1}[a-z]", _sut.FullName);
        }

        [Fact]
        public void StartWithDefaultHealth()
        {
            Assert.Equal(100, _sut.Health);
        }

        [Fact]
        public void IncreaseHelpAfterSleeping()
        {
            _sut.Sleep();
            Assert.InRange<int>(_sut.Health, 101, 200);
        }

        [Fact]
        public void NotHaveNicknamebyDefault()
        {
            Assert.Null(_sut.Nickname);
        }

        [Fact]
        public void HaveALongBow()
        {
            Assert.Contains("Long Bow", _sut.Weapons);
        }

        [Fact]
        public void NotHaveTank()
        {
            Assert.DoesNotContain("Tank", _sut.Weapons);
        }
        [Fact]
        public void HaveAtleastOneKindOfSword()
        {
            Assert.Contains(_sut.Weapons, weapon => weapon.Contains("Sword"));
        }
        [Fact]
        public void HaveAllExpectedWeapons()
        {
            var expected = new List<string>
            {
                "Long Bow",
                "Short Bow",
                "Short Sword",
            };
            Assert.Equal(expected, _sut.Weapons);
        }
        //test against every item in the collection
        [Fact]
        public void HaveNoEmptyDefaultWepons()
        {
            Assert.All(_sut.Weapons, weapon => Assert.False(string.IsNullOrWhiteSpace(weapon)));
        }
        [Fact]
        public void RaiseSleptEvent()
        {
            Assert.Raises<EventArgs>(
                handler => _sut.PlayerSlept += handler,
                handler => _sut.PlayerSlept -= handler,
                () => _sut.Sleep());
        }

        [Fact]
        public void RaisePropertyChangedEvent()
        {
            Assert.PropertyChanged(
                _sut,
                "Health",
                () => _sut.TakeDamage(10));
        }

        public void Dispose()
        {
            //this is empty to be implemented in future
        }
    }
}
