using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using TradeArt.Business;
using TradeArt.Contracts;
using Xunit;

namespace TradeArt.UnitTests.Services
{
	public class HashCalculatorTest
	{
		private readonly IHashCalculator _hashService;
        private readonly string _dir;
		public HashCalculatorTest()
		{
            _hashService = new HashCalculator();
            _dir = PathDirectory;
		}

        public static string PathDirectory
        {
            get
            {
                var basePath = Path.GetFullPath(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"../../../.."));
                return $"{basePath}/TradeArt/Resources/input.txt";
            }
        }

        [Fact]
        public void Should_Validate_File_Path()
        {
            //Arrange
            var filePath = _dir;

            //Act
            var fileExists = File.Exists(filePath);

            //Assert
            Assert.True(fileExists);
        }

        [Theory]
        [InlineData("5551788da24fd0dd81c101ccf91545a400479964752f64964e4b7b76e3f6acdb002359a6982c3404d5c881ecda8f656cbd051afb8c1052c553f8b84adc6ce450")]
        public async Task Should_CalculateHash_And_CorrectResults(string expected)
        {
            //Arrange
            var filePath = _dir;

            //Act
            var hashed = await _hashService.CalculateShaAsync(filePath);

            //Assert
            Assert.NotNull(hashed);
            Assert.Equal(expected, hashed);
        }

        [Fact]
        public async Task Should_Throw_ArgumentNullException_For_EmptyOrNull_String()
        {
            // Arrange, Act, Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _hashService.CalculateShaAsync(string.Empty));
        }

        [Theory]
        [InlineData("thisFileDoesNotExist")]
        public async Task Should_Throw_FileNotFoundException_For_Innvalid_FilePath(string filePath)
        {
            // Arrange, Act, Assert
            await Assert.ThrowsAsync<FileNotFoundException>(() => _hashService.CalculateShaAsync(filePath));
        }
    }
}

