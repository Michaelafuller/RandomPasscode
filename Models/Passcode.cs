using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RandomPasscode
{
    public class Passcode
    {
        public string RandPass { get; set; }

        public Passcode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            RandPass = new string(Enumerable.Repeat(chars, 14)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            
        }
    }
}