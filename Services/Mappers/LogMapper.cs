using SwiftParser.Data.Entities;
using SwiftParser.DTOs;

namespace SwiftParser.Services.Mappers;

public static class LogMapper
{
    extension(Log log)
    {
        public LogDTO ToDTO()
        {
            return new LogDTO
            {
                Id = log.Id,
                Message = log.Message,
                Timestamp = log.Timestamp
            };
        }
    }

    extension(LogDTO logDTO)
    {
        public Log ToEntity()
        {
            return new Log
            {
                Message = logDTO.Message,
                Timestamp = logDTO.Timestamp
            };
        }
    }
}