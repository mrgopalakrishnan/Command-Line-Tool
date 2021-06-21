using AutoMoq;
using CommandLineTool;
using CommandLineTool.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Unittests
{
    public class CliTests
    {
        [Fact]
        public void TestMultipleInstanceNotAllowed()
        {
            _ = new Cli(typeof(Mocks.MockApp));
            _ = Assert.Throws<InstanceAlreadyExistException>(() => new Cli());
        }

        [Fact]
        public void TestAppAttributeMandatory()
        {
            _ = Assert.Throws<MissingAppAttributeException>(() => new Cli(typeof(Mocks.MockNotApp)));
        }
    }
}
