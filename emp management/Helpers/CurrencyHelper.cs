// CurrencyHelper.cs - Helper class for formatting currency in Indian Rupees
using System.Globalization;

namespace emp_management.Helpers
{
    public static class CurrencyHelper
    {
        private const string RupeeSymbol = "?";
        
        // Format decimal to INR currency format
        public static string FormatINR(decimal amount)
        {
            // Indian numbering system with lakhs and crores
            return RupeeSymbol + amount.ToString("N2", new CultureInfo("en-IN"));
        }

        // Format decimal to INR without decimal places
        public static string FormatINRWhole(decimal amount)
        {
            return RupeeSymbol + amount.ToString("N0", new CultureInfo("en-IN"));
        }
    }
}
