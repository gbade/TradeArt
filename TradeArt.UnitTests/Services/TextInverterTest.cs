using System;
using TradeArt.Business;
using TradeArt.Contracts;
using Xunit;

namespace TradeArt.UnitTests.Services
{
	public class TextInverterTest
	{
        private ITextInverter _inverter;
      
        public TextInverterTest()
		{
            _inverter = new TextInverter();
        }

        [Fact]
        public void Should_Invert_Text()
        {
            // Arrange
            var text = "The quick brown fox jumps over the lazy dog.";

            // Act
            var inverted = _inverter.InvertText(text);

            // Assert
            Assert.Equal(".god yzal eht revo spmuj xof nworb kciuq ehT", inverted);
        }

        [Fact]
        public void Should_Throw_ArgumentException_On_Empty_Text_String()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => _inverter.InvertText(string.Empty));
        }
    }
}

