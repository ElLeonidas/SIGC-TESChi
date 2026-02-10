using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SIGC_TESChi
{
    public static class HashHelper
    {
        public static string CrearHashPBKDF2(string password)
        {
            int iterations = 100000;
            byte[] salt = new byte[16];

            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                return $"{iterations}:{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
            }
        }



        public static bool VerificarHashPBKDF2(string password, string hashAlmacenado)
        {
            var partes = hashAlmacenado.Split(':');
            if (partes.Length != 3) return false;

            int iterations = int.Parse(partes[0]);
            byte[] salt = Convert.FromBase64String(partes[1]);
            byte[] hashOriginal = Convert.FromBase64String(partes[2]);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] hashCalculado = pbkdf2.GetBytes(hashOriginal.Length);

                for (int i = 0; i < hashOriginal.Length; i++)
                    if (hashCalculado[i] != hashOriginal[i])
                        return false;
            }
            return true;
        }
    }

}
