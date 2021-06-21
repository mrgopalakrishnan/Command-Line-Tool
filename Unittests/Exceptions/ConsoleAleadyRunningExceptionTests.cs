using AutoMoq;
using CommandLineTool.Exceptions;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace Unittests.Exceptions
{
    public class ConsoleAleadyRunningExceptionTests
    {
        [Fact]
        public void TestExceptionWithoutParam()
        {
            Action testCode = () => { throw new ConsoleAleadyRunningException(); };

            var ex = Record.Exception(testCode);

            Assert.NotNull(ex);
            Assert.IsType<ConsoleAleadyRunningException>(ex);
            ex.Message.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void TestExceptionWithParam()
        {
            Action testCode = () => { throw new ConsoleAleadyRunningException("mymessage"); };

            var ex = Record.Exception(testCode);

            Assert.NotNull(ex);
            Assert.IsType<ConsoleAleadyRunningException>(ex);
            ex.Message.Should().Be("mymessage");
        }
    }
}
