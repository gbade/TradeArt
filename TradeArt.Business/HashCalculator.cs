using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TradeArt.Contracts;

namespace TradeArt.Business
{
	public class HashCalculator : IHashCalculator
	{
        public async Task<string> CalculateShaAsync(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            if (!File.Exists(path))
                throw new FileNotFoundException(nameof(path));

            using var sha512 = SHA512.Create();
            using var stream = new FileStream(path, FileMode.Open);

            var hash = await sha512.ComputeHashAsync(stream);

            var sb = new StringBuilder();

            foreach (var i in hash)
                sb.Append(i.ToString("x2"));

            return sb.ToString();
        }
    }
}

