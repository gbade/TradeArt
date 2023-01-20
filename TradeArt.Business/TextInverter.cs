using System;
using TradeArt.Contracts;

namespace TradeArt.Business
{
	public class TextInverter : ITextInverter
	{
        public string InvertText(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException(nameof(text));

            var chars = text.ToCharArray();
            Array.Reverse(chars);

            return new string(chars);
        }
    }
}

