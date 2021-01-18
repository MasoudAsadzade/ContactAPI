using ContactAPI.Application.Interfaces.Shared;
using System;

namespace ContactAPI.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}