namespace SwiftParser.Shared;

public static class ErrorConstants
{
    public static class SwiftMessages
    {
        public const string NoFileProvided = "Upload rejected, no file provided!";
        public const string InvalidFileType = "Invalid file type. Only .txt files are accepted.";
        public const string ParsingFailed = "Failed to parse the Swift MT103 message. Make sure the file you uploaded is not empty!";
        public const string InvalidOperation = "Deleting the message with id {0} was not completed!";
        public const string MessageNotFound = "Message with id {0} was not found!";
    }

    public static class LogMessages
    {
        public const string InvalidOperation = "Deleting the log with id {0} was not completed!";
        public const string LogNotFound = "Log with id {0} was not found!";
    }
}