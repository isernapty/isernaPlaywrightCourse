using Moq;
using System;
using TechTalk.SpecFlow;
using static RateCalculator;
using FluentAssertions;

namespace Playwright_Project.Steps
{
    [Binding]
    public class PaymentUnitTestStepDefinitions
    {
        RateCalculator rateCalculator = new RateCalculator();
        Mock<IDateTimeProvider> dateTimeProviderMock = new Mock<IDateTimeProvider>();
        decimal actual;

        [Given(@"its (.*)")]
        public void GivenItsDayOfTheWeek(string day)
        {

            if (day == "monday")
            {
                dateTimeProviderMock.Setup(m => m.DayOfWeek())
                                .Returns(DayOfWeek.Monday);
            } if (day == "sunday")
            {
                dateTimeProviderMock.Setup(m => m.DayOfWeek())
                                .Returns(DayOfWeek.Sunday);
            }
            
        }

        [When(@"I calculate the rate of the pay for (.*)m")]
        public void WhenICalculateTheRateOfThePayForM(decimal p0)
        {
            actual = rateCalculator.GetPayRate(p0, dateTimeProviderMock.Object);
        }

        [Then(@"I obtain the usual pay: (.*)m")]
        public void ThenIObtainTheUsualPayM(decimal p0)
        {
            actual.Should().Be(p0);
        }

        [Then(@"I obtain a 25% higher pay: (.*)m")]
        public void ThenIObtainAHigherPayM(decimal p0)
        {
            actual.Should().Be(p0);
        }
    }
}
