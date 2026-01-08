using System.Text.RegularExpressions;

namespace CManager.Presentation.ConsoleApp.Helpers;

// Validation types for user input
public enum ValidationType
{
    Required,
    Email,
}

public static class InputHelper
{
    // validates input from user
    public static string ValidateInput(string fieldName, ValidationType validationType)
    {
        while (true)
        {
            Console.Write($"{fieldName}: ");
            var input = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine($"{fieldName} is required. Press any key to try again....");
                Console.ReadKey();
                continue;
            }

            var (isValid, errorMessage) = ValidateType(input, validationType);

            if (isValid)
                return input;

            Console.WriteLine($"{errorMessage}. Press any key to continue:");
            Console.ReadKey();
        }

    }


    // validates input data based on specified validation type
    private static (bool isValid, string errorMessage) ValidateType(string input, ValidationType type)
    {
        switch (type)
        {
            case ValidationType.Required:
                return (true, "");


            case ValidationType.Email:
                if (IsValidEmail(input))
                {
                    return (true, "");
                }
                else
                {
                    return (false, "Invalid email. Use name@example.com ");
                }

            default:
                return (true, "");
        }

    }

    // validates email format
    private static bool IsValidEmail(string input)
    {
        var pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        return Regex.IsMatch(input, pattern);
    }

}
