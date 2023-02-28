using System.Windows.Media;

namespace CryptoGraphy.Models
{
    public class HashResultModel
    {
        public int Id { get; set; } = int.MaxValue;

        public string Algorithm { get; set; } = string.Empty;

        public string HashedValue { get; set; } = string.Empty;

        public Brush? BackColor { get; set; }
    }
}
