using AutoMoq;
using CommandLineTool.Exceptions;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace Unittests.Exceptions
{
    public class InstanceAlreadyExistExceptionTests
    {
        [Fact]
        public void TestExceptionWithoutParam()
        {
            Action testCode = () => { throw new InstanceAlreadyExistException(); };

            var ex = Record.Exception(testCode);

            Assert.NotNull(ex);
            Assert.IsType<InstanceAlreadyExistException>(ex);
            ex.Message.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void TestExceptionWithParam()
        {
            Action testCode = () => { throw new InstanceAlreadyExistException("mymessage"); };

            var ex = Record.Exception(testCode);

            Assert.NotNull(ex);
            Assert.IsType<InstanceAlreadyExistException>(ex);
            ex.Message.Should().Be("mymessage");
        }
    }
}
