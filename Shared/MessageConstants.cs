namespace SwiftParser.Shared;

public static class MessageConstants
{
    public const string GetAllMessagesSummary = "Get all parsed Swift MT103 messages";
    public const string GetAllMessagesDescription = "Returns a list of all parsed Swift MT103 messages stored in the database";
    public const string DatabaseEmpty = "No Swift messages found in the database!";
    public const string MessageReceived = "Upload request received. File info: {0} with size {1} KB";
    public const string SwiftApiTag = "Endpoints for uploading and processing Swift MT103 financial messages";
    public const string UploadMethodSummary = "Upload a Swift MT103";
    public const string UploadMethodDescription = "Accepts a .txt file containing a Swift MT103 free-format message";
}