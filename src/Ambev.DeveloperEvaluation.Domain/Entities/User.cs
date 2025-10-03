using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a user in the system with authentication and profile information.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class User : BaseEntity, IUser
{
    /// <summary>
    /// Gets the user's full name.
    /// </summary>
    public string Username { get; private set; }

    /// <summary>
    /// Gets the user's email address.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets the user's phone number.
    /// </summary>
    public string Phone { get; private set; }

    /// <summary>
    /// Gets the hashed password for authentication.
    /// </summary>
    public string Password { get; private set; }

    /// <summary>
    /// Gets the user's role in the system.
    /// </summary>
    public UserRole Role { get; private set; }

    /// <summary>
    /// Gets the user's current status.
    /// </summary>
    public UserStatus Status { get; private set; }

    /// <summary>
    /// Gets the date and time when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets the date and time of the last update to the user's information.
    /// </summary>
    public DateTime? UpdatedAt { get; private set; }

    string IUser.Id => Id.ToString();
    string IUser.Username => Username;
    string IUser.Role => Role.ToString();

    /// <summary>
    /// Private constructor for exclusive use by EF Core.
    /// </summary>
    private User()
    {
        Username = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        Password = string.Empty;
    }

    /// <summary>
    /// Public constructor that ensures the creation of a valid User entity.
    /// </summary>
    public User(string username, string email, string phone, UserRole role, UserStatus status)
    {
        Username = username;
        Email = email;
        Phone = phone;
        Role = role;
        Status = status;
        CreatedAt = DateTime.UtcNow;
        Password = string.Empty;

        ValidateState("constructor");
    }

    /// <summary>
    /// Sets the user's password. The password must already be hashed from the application layer.
    /// </summary>
    /// <param name="hashedPassword">The encrypted password.</param>
    public void SetPassword(string hashedPassword)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword))
            throw new DomainException("Password hash cannot be empty.");

        Password = hashedPassword;
    }

    /// <summary>
    /// Activates the user account.
    /// </summary>
    public void Activate()
    {
        Status = UserStatus.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Deactivates the user account.
    /// </summary>
    public void Deactivate()
    {
        Status = UserStatus.Inactive;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Blocks the user account.
    /// </summary>
    public void Suspend()
    {
        Status = UserStatus.Suspended;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Performs full entity validation before persistence.
    /// </summary>
    public void Validate()
    {
        ValidateState("constructor, default");
    }

    /// <summary>
    /// Private method that runs the domain validator and throws a DomainException on failure.
    /// </summary>
    private void ValidateState(string ruleSet)
    {
        var validator = new UserValidator();
        var validationResult = validator.Validate(this, options => options.IncludeRuleSets(ruleSet.Split(',')));

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new DomainException($"User entity validation failed: {errors}");
        }
    }
}