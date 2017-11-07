using System;
using System.Text;


	public class Encryptor 
	{

		public static string Encode(string plainText)
		{
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var temp = Convert.ToBase64String(plainTextBytes);
            var code = Encoding.UTF8.GetBytes(temp);

            return Convert.ToBase64String(code);
		}

		public static string Decode(string base64EncodedData)
		{
			var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            var temp = Encoding.UTF8.GetString(base64EncodedBytes);
            var encode = Convert.FromBase64String(temp);

			return Encoding.UTF8.GetString(encode);
		}
}
