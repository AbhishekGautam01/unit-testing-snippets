using CreditCardApplications;
using Moq;
using System;
using Xunit;

namespace XUnit_Moq.Tests
{
    public class CreditCardApplicationEvaluatorShould
    {
        [Fact]
        public void AcceptHighIncomeApplication()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { GrossAnnualIncome = 100_100 };
            CreditCardApplicationDecision decision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.AutoAccepted, decision);
        }

        [Fact]
        public void ReferYoungApplications()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var application = new CreditCardApplication { Age = 19 };
            CreditCardApplicationDecision decision = sut.Evaluate(application);
            Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, decision);
        }

        [Fact]
        public void DeclineLowIncomeApplications()
        {
            Mock<IFrequentFlyerNumberValidator> mockValidator = new Mock<IFrequentFlyerNumberValidator>();
            //mockValidator.Setup(x => x.IsValid("x")).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            //mockValidator.Setup(x => x.IsValid(It.Is<string>(number => number.StartsWith("y")))).Returns(true);
            //mockValidator
            //    .Setup(x => x.IsValid(It.IsInRange<string>("a", "z", Moq.Range.Inclusive)))
            //    .Returns(true);
            //mockValidator
            //    .Setup(x => x.IsValid(It.IsIn<string>("z", "y", "x")))
            //    .Returns(true);
            mockValidator
                .Setup(x => x.IsValid(It.IsRegex("[a-z]")))
                .Returns(true);

            var sut = new CreditCardApplicationEvaluator(mockValidator.Object);
            var appliction = new CreditCardApplication { GrossAnnualIncome = 19_999, Age = 42, FrequentFlyerNumber = "y" };
            CreditCardApplicationDecision decision = sut.Evaluate(appliction);
            Assert.Equal(CreditCardApplicationDecision.AutoDeclined, decision);
        }
    }
}
