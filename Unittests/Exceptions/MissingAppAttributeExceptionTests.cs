using AutoMoq;
using CommandLineTool.Exceptions;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace Unittests.Exceptions
{
    public class MissingAppAttributeExceptionTests
    {
        [Fact]
        public void TestExceptionWithoutParam()
        {
            Action testCode = () => { throw new MissingAppAttributeException(); };

            var ex = Record.Exception(testCode);

            Assert.NotNull(ex);
            Assert.IsType<MissingAppAttributeException>(ex);
            ex.Message.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void TestExceptionWithParam()
        {
            Action testCode = () => { throw new MissingAppAttributeException("mymessage"); };

            var ex = Record.Exception(testCode);

            Assert.NotNull(ex);
            Assert.IsType<MissingAppAttributeException>(ex);
            ex.Message.Should().Be("mymessage");
        }
    }
}
