namespace task01
{
    public static class StringExtensions
    {
        public static bool IsPalindrome(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            input = input.ToLower();
            string newString = string.Empty;

            newString = new string(input.Where(c => !(char.IsPunctuation(c) || char.IsWhiteSpace(c))).ToArray());
            string reversed = new string(newString.Reverse().ToArray());

            return newString == reversed;
        }
    }
}
