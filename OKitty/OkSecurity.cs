// Imports

using System.Security.Cryptography;

namespace OKitty;

// OkSecurity

public static class OkSecurity
{
    // Classes

    public static class OPassword
    {
        // Enums
    
        public enum OHashAlgorithm
        {
            Md5,
            Sha1,
            Sha256,
            Sha384,
            Sha512
        }
        
        public enum OPasswordStrength
        {
            Empty,
            VeryWeak,
            Weak,
            Medium,
            Strong,
            VeryStrong
        }
        
        // Properties

        public static OHashAlgorithm HashAlgorithm { get; set; } = OHashAlgorithm.Sha256;
        public static int Iterations { get; set; } = 100000;
        public static int KeySize { get; set; } = 32;
        public static int SaltSize { get; set; } = 16;
        public static int MinLength { get; set; } = 8;
        public static bool RequireUppercase { get; set; } = true;
        public static bool RequireLowercase { get; set; } = true;
        public static bool RequireNumbers { get; set; } = true;
        public static bool RequireSpecialChars { get; set; } = true;
        
        // Functions

        public static string GeneratePassword(int length = 16, string? charset = null)
        {
            string characters = charset ?? "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_=+[]{}|;:,.<>?";
            
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] bytes = new byte[length];
            
            rng.GetBytes(bytes);
    
            char[] password = new char[length];
            
            for (int i = 0; i < length; i++)
                password[i] = characters[bytes[i] % characters.Length];
    
            return new string(password);
        }

        public static string HashPassword(string password)
        {
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[SaltSize];
            
            rng.GetBytes(salt);

            HashAlgorithmName algorithmName = HashAlgorithm switch
            {
                OHashAlgorithm.Md5 => HashAlgorithmName.MD5,
                OHashAlgorithm.Sha1 => HashAlgorithmName.SHA1,
                OHashAlgorithm.Sha256 =>  HashAlgorithmName.SHA256,
                OHashAlgorithm.Sha384 =>  HashAlgorithmName.SHA384,
                OHashAlgorithm.Sha512 =>  HashAlgorithmName.SHA512
            };
            using Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, algorithmName);
            byte[] key = pbkdf2.GetBytes(KeySize);
            
            return $"{HashAlgorithm}.{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
        }
        
        public static bool VerifyPassword(string password, string hashed)
        {
            string[] parts = hashed.Split('.', 4);
            
            if (parts.Length != 4)
                return false;

            if (!Enum.TryParse(parts[0], out OHashAlgorithm algorithm))
                return false;

            HashAlgorithmName algorithmName = algorithm switch
            {
                OHashAlgorithm.Md5 => HashAlgorithmName.MD5,
                OHashAlgorithm.Sha1 => HashAlgorithmName.SHA1,
                OHashAlgorithm.Sha256 =>  HashAlgorithmName.SHA256,
                OHashAlgorithm.Sha384 =>  HashAlgorithmName.SHA384,
                OHashAlgorithm.Sha512 =>  HashAlgorithmName.SHA512
            };
            int iterations = Convert.ToInt32(parts[1]);
            byte[] salt = Convert.FromBase64String(parts[2]);
            byte[] storedKey = Convert.FromBase64String(parts[3]);
            
            using Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, algorithmName);
            byte[] computedKey = pbkdf2.GetBytes(storedKey.Length);

            return CryptographicOperations.FixedTimeEquals(storedKey, computedKey);
        }

        public static string? ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return "Password is empty.";

            if (password.Length < MinLength)
                return $"Password must be at least {MinLength} characters long.";

            if (RequireUppercase && !password.Any(char.IsUpper))
                return $"Password must contain at least one uppercase letter.";

            if (RequireLowercase && !password.Any(char.IsLower))
                return $"Password must contain at least one lowercase letter.";

            if (RequireNumbers && !password.Any(char.IsDigit))
                return $"Password must contain at least one number.";

            if (RequireSpecialChars && password.All(char.IsLetterOrDigit))
                return $"Password must contain at least one special character.";
            
            return null;
        }

        public static OPasswordStrength CheckPasswordStrength(string password)
        {
            if (string.IsNullOrEmpty(password))
                return OPasswordStrength.Empty;
            
            int score = 0;
        
            if (password.Length >= MinLength)
                score++;
            
            if (password.Any(char.IsUpper))
                score++;
            
            if (password.Any(char.IsLower))
                score++;
            
            if (password.Any(char.IsDigit))
                score++;
            
            if (password.Any((char character) => !char.IsLetterOrDigit(character)))
                score++;
        
            return score switch
            {
                <= 1 => OPasswordStrength.VeryWeak,
                2 => OPasswordStrength.Weak,
                3 => OPasswordStrength.Medium,
                4 => OPasswordStrength.Strong,
                _ => OPasswordStrength.VeryStrong
            };
        }
    }
}