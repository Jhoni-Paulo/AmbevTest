using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

/// <summary>
/// Contains unit tests for the CreateUserHandler class.
/// </summary>
public class CreateUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly CreateUserHandler _handler;

    public CreateUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _handler = new CreateUserHandler(_userRepository, _mapper, _passwordHasher);
    }

    [Fact(DisplayName = "Given valid command When creating user Then should return success result")]
    public async Task Handle_WithValidCommand_ShouldReturnSuccessResult()
    {
        // Arrange
        var command = CreateUserHandlerTestData.GenerateValidCommand();
        var user = UserTestData.GenerateValidUser();

        _userRepository.GetByEmailAsync(command.Email).ReturnsNull();
        _passwordHasher.HashPassword(command.Password).Returns("hashed_password");
        _userRepository.CreateAsync(Arg.Any<User>()).Returns(user);

        var expectedResult = new CreateUserResult { Id = user.Id };
        _mapper.Map<CreateUserResult>(user).Returns(expectedResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(user.Id);
        await _userRepository.Received(1).CreateAsync(Arg.Any<User>());
    }

    [Fact(DisplayName = "Given an existing email When creating user Then should throw DomainException")]
    public async Task Handle_WithExistingEmail_ShouldThrowDomainException()
    {
        // Arrange
        var command = CreateUserHandlerTestData.GenerateValidCommand();
        var existingUser = UserTestData.GenerateValidUser();
        _userRepository.GetByEmailAsync(command.Email).Returns(existingUser);

        // Act
        Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DomainException>()
            .WithMessage($"User with email {command.Email} already exists.");
    }

    [Fact(DisplayName = "Given command that creates invalid entity When creating user Then should throw DomainException")]
    public async Task Handle_WithInvalidEntityData_ShouldThrowDomainExceptionFromEntity()
    {
        // Arrange
        var command = CreateUserHandlerTestData.GenerateValidCommand();
        command.Username = "";

        _userRepository.GetByEmailAsync(command.Email).ReturnsNull();

        // Act       
        Func<Task> act = () => _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<DomainException>();
    }
}