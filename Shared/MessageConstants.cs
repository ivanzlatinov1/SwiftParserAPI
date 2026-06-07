namespace SwiftParser.Shared;

public static class MessageConstants
{
    public static class SwiftMessages
    {
        public const string GetAllMessagesSummary = "Get all parsed Swift MT103 messages";
        public const string GetAllMessagesDescription = "Returns a list of all parsed Swift MT103 messages stored in the database";
        public const string DatabaseEmpty = "No Swift messages found in the database!";
        public const string MessageReceived = "Upload request received. File info: {0} with size {1} KB";
        public const string SwiftApiTag = "Endpoints for uploading and processing Swift MT103 financial messages";
        public const string UploadMethodSummary = "Upload a Swift MT103";
        public const string UploadMethodDescription = "Accepts a .txt file containing a Swift MT103 free-format message";
        public const string ParsingComplete = "Swift message parsed successfully!";
    }

    public static class LogMessages
    {
        public const string LogApiTag = "Endpoint for extracting logs from the database";
        public const string GetAllLogsSummary = "Query all logs";
        public const string GetAllLogsDescription = "Returns a list of all logs stored in the database";
        public const string SuccessfulOperation = "Successfully applied the operation!";
        public const string DatabaseEmpty = "No logs found in the database!";
        public const string GetLogByIdSummary = "Get log by ID";
        public const string GetLogByIdDescription = "Retrieves a log entry using its unique identifier";
        public const string DeleteLogSummary = "Delete log";
        public const string DeleteLogDescription = "Deletes a log entry using its unique identifier. Returns 404 if the log does not exist";
    }

}