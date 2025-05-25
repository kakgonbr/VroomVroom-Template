using FluentAssertions;

namespace VroomVroom.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            VroomVroom.Server.Program.doSomething().Should().BeTrue();
        }
    }
}