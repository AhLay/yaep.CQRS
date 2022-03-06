using System;
using YAEP.CQRS.Abstractions;

namespace CQRSTests
{
    public class FakeResult : IResult
    {
        public string Message { get; private set; }

        public bool IsSuccess => Message.IsNullOrEmpty();
    }
}
