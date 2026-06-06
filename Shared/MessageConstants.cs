namespace SwiftParser.Shared;

public static class MessageConstants
{
    public const string MessageReceived = "Upload request received. File info: {0} with size {1} KB";
    public const string SwiftApiTag = "Endpoints for uploading and processing Swift MT103 financial messages";
    public const string UploadMethodSummary = "Upload a Swift MT103";
    public const string UploadMethodDescription = "Accepts a .txt file containing a Swift MT103 free-format message";
}