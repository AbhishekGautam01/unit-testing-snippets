using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestNinja.NUnitTests.Fundamentals
{
    [TestFixture]
    class StackTests
    {
        //we will not work with any private members of the class
      
            [Test]
        public void Push_ArgumentIsNull_ThrowsArgumentNullException()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();
            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ArgumentIsValid_PushArgumentToStack()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();
            stack.Push("a");
            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Count_WhenStackIsEmpty_Return0()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();
            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void POP_WhenCalledOnEmptyStack_ThrowsInvalidOperationException()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();
            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void POP_StackWithAFewStack_ReturnsObjectOnTop()
        {

            //Arrange
            var stack = new TestNinja.Fundamentals.Stack<string>();
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            //Act
            var result = stack.Pop();

            //Assert
            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void POP_StackWithAFewStack_RemoveObjectOnTop()
        {
            //Arrange
            var stack = new TestNinja.Fundamentals.Stack<string>();
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            //Act
            stack.Pop();

            //Assert
            Assert.That(stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_WhenCalledOnEmptyStack_ThrowInvalidOperationException()
        {
            var stack = new TestNinja.Fundamentals.Stack<string>();
            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_WithFewObjects_ReturnsTheTopObject()
        {
            //Arrange
            var stack = new TestNinja.Fundamentals.Stack<string>();
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            //Act
            var result = stack.Peek();

            //Assert
            Assert.That(result, Is.EqualTo("c"));

        }

        [Test]
        public void Peek_StackWithFewObjects_DoesnotRemoveObjectFromTopOfStack()
        {
            //Arrange
            var stack = new TestNinja.Fundamentals.Stack<string>();
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            //Act
            var result = stack.Peek();

            //Assert
            Assert.That(stack.Count, Is.EqualTo(3));
        }
    }
}
