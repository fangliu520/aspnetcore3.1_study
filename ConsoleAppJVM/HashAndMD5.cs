using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppJVM
{
    public class HashAndMD5
    {
		public string GetMD5(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException(nameof(input));
			}

			using (var md5 = MD5.Create())
			{
				var buffer = Encoding.UTF8.GetBytes(input);
				var hashResult = md5.ComputeHash(buffer);
				return BitConverter.ToString(hashResult).Replace("-", string.Empty);
			}
		}

		public string GetSHA1(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException(nameof(input));
			}

			using (var sha1 = SHA1.Create())
			{
				var buffer = Encoding.UTF8.GetBytes(input);
				var hashResult = sha1.ComputeHash(buffer);
				return BitConverter.ToString(hashResult).Replace("-", string.Empty);
			}
		}
	}
}
