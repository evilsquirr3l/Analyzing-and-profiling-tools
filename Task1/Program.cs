using System.Diagnostics;
using Task1;

byte[] GenerateSalt(int length)
{
    var salt = new byte[length];
    System.Security.Cryptography.RandomNumberGenerator.Fill(salt);
    return salt;
}

var salt = GenerateSalt(16);

var passwords = new[]
{
    "abc1234!John",
    "Asdfghjklqwertyuiozxcvbnnm;'/.,mnbvcxzlkjhgfdsapoiuytrewq;",
    "456",
    "@#$%^&*()_+{}|:<>?",
    "SecurePass21",
    "98765432109876543210-98765432109876543210987654321098765432109876543210",
    "987987987987987987987987987987987987987987987987987987987987987987987987987987987",
    "RandomWord890!",
    "ZXCVBNM<>?:LKJHGFDSAOIUYTREWQ",
    "789",
    "!@#$%^&*()_+?><:{}|",
    "AnotherPassword34",
    "567567567567567567567567567567567567567567567567567567567567567567567567567567567",
};

for (var i = 0; i < passwords.Length; i += 1)
{
    var sw = new Stopwatch();

    sw.Start();
    var passwordHashOriginal = Profiling.GeneratePasswordHashUsingSalt(passwords[i], salt);
    sw.Stop();
    var originalElapsedTime = sw.Elapsed;

    sw.Restart();
    var passwordHashUpdated = Profiling.GeneratePasswordHashUsingSaltUpdated(passwords[i], salt);
    sw.Stop();
    var updatedElapsedTime = sw.Elapsed;

    var diff = originalElapsedTime.CompareTo(updatedElapsedTime);
    var result = diff switch
    {
        < 0 => $"Updated method was faster by: {updatedElapsedTime - originalElapsedTime}",
        > 0 => $"Original method was faster by: {originalElapsedTime - updatedElapsedTime}",
        _ => "Both methods took the same time.",
    };

    Console.WriteLine(result);
}
