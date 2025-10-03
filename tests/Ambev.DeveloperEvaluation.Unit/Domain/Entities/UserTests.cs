using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the User entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class UserTests
{
    /// <summary>
    /// Tests that when a suspended user is activated, their status changes to Active.
    /// </summary>
    [Fact(DisplayName = "User status should change to Active when activated")]
    public void Given_SuspendedUser_When_Activated_Then_StatusShouldBeActive()
    {
        // Arrange
        var user = UserTestData.GenerateValidUserWithStatus(UserStatus.Suspended);

        // Act
        user.Activate();

        // Assert
        user.Status.Should().Be(UserStatus.Active);
    }

    /// <summary>
    /// Tests that when an active user is suspended, their status changes to Suspended.
    /// </summary>
    [Fact(DisplayName = "User status should change to Suspended when suspended")]
    public void Given_ActiveUser_When_Suspended_Then_StatusShouldBeSuspended()
    {
        // Arrange
        var user = UserTestData.GenerateValidUserWithStatus(UserStatus.Active);

        // Act
        user.Suspend();

        // Assert
        user.Status.Should().Be(UserStatus.Suspended);
    }

    /// <summary>
    /// Tests that validation passes when all user properties are valid.
    /// </summary>
    [Fact(DisplayName = "Given valid data When creating user Then constructor should not throw exception")]
    public void Given_ConstructorWithValidData_ShouldNotThrowException()
    {
        // Arrange
        var user = UserTestData.GenerateValidUser();

        // Act
        Action act = () => user.Validate();


        // Assert
        act.Should().NotThrow();
    }

    /// <summary>
    /// Tests that validation fails when user properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Given invalid data When creating user Then constructor should throw DomainException")]
    public void Given_ConstructorWithInvalidData_ShouldThrowDomainException()
    {
        // Arrange
        Action act = () => new User(
            "",
            UserTestData.GenerateInvalidEmail(),
            UserTestData.GenerateInvalidPhone(),
            UserRole.None,
            UserStatus.Unknown
        ).SetPassword(UserTestData.GenerateInvalidPassword());

        // Act & Assert
        act.Should().Throw<DomainException>();
    }
}
