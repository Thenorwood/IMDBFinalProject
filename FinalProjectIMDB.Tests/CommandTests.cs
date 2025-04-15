using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalProjectIMDB.Commands;
using System;

namespace FinalProjectIMDB.Tests
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void RelayCommand_ExecutesAction()
        {
            // Arrange
            bool actionExecuted = false;
            var command = new RelayCommand(_ => actionExecuted = true);

            // Act
            command.Execute(null);

            // Assert
            Assert.IsTrue(actionExecuted);
        }

        [TestMethod]
        public void RelayCommand_CanExecuteReturnsTrue_WhenPredicateIsNull()
        {
            // Arrange
            var command = new RelayCommand(_ => { });

            // Act
            bool canExecute = command.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute);
        }

        [TestMethod]
        public void RelayCommand_CanExecuteUsesProvidedPredicate()
        {
            // Arrange
            var command = new RelayCommand(
                _ => { },
                param => param != null
            );

            // Act & Assert
            Assert.IsFalse(command.CanExecute(null));
            Assert.IsTrue(command.CanExecute("not null"));
        }
    }
}
