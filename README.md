# ThomasW.Domain.SharedKernel.Results

Indicate the success of domain or application operations without throwing slow
and expensive exceptions.

## Installation

This package can be installed via NuGet.

## Usage

Creation of results is achieved by accessing the static methods on the `Result`
class.

For operations that do not return a value, such as application commands, you 
can create basic result objects using the `Fail<T>()` and `Success()` methods:

```c#
public async Task<Result> CreateUser(Guid userId, User user)
{
    if (userId == default)
    {
        return result.Fail<InvalidEntityIdFailure>();
    }

    await this._repository.AddUser(userId, user);
    
    return result.Success();
}
```

In the above example, a type is passed into the `Fail<T>()` method that
indicates the failure reason. This type **must** inherit the `FailureReason`
abstract class and have a parameterless constructor.

For operations that _do_ return a value, such as application queries, you can
create typed result objects using the `Fail<TValue, TReason>()` and
`Success<T>()` methods:


```c#
public async Task<Result<User>> GetUser(Guid userId)
{
    User? user = await this._repository.GetUser(userId);
    
    if (user == null)
    {
        return Result.Fail<User, EntityNotFoundFailure>();
    }
    
    return Result.Success(user);
}
```

In the preceding example, another type is passed into the
`Fail<TValue, TReason>()` method to indicate the type of value that would have
been returned had the operation been successful.

As the value is passed directly into the `Success<T>()` method, its type can be
inferred and thus does not have to be explicitly specified.

For instances in which there may be multiple possible failure reasons,
specifying the value type in each call to `Fail<TValue, TReason>()` will soon
become laborious. To make this easier, you can create pending typed results
with the `OfType<T>()` method and specify the failure reason when necessary:

```c#
public async Task<Result<User>> GetUser(Guid userId)
{
    PendingResult<User> result = Result.OfType<User>();
    
    if (userId == default)
    {
        return result.Fail<InvalidEntityIdFailure>();
    }

    User? user = await this._repository.GetUser(userId);
    
    if (user == null)
    {
        return result.Fail<EntityNotFoundFailure>();
    }
    
    return result.Success(user);
}
```

When consuming results, you can determine their success with the `IsSuccessful`
and `IsFailed` properties, which will indicate whether the `Value` and
`FailureReason` properties are `null`:

```c#
public async IActionResult Get(Guid userId)
{
    Result<User> user = await this._userService.Get(userId);
    
    if (result.IsSuccessful)
    {
        return this.Ok(result.Value);
    }
    
    if (result.FailureReason is EntityNotFoundFailure)
    {
        return this.NotFound();
    }
    
    return this.BadRequest();
}
```

Here, we check that the result is successful, which will tell the compiler that
the `Value` property is _not_ `null`, and the `FailureReason` property _is_
`null`.

Conversely, if we checked that the operation failed, this will tell the
compiler that the `FailureReason` property is _not_ `null`, and the `Value`
property _is_ `null`:

```c#
public async IActionResult Get(Guid userId)
{
    Result<User> user = await this._userService.Get(userId);
    
    if (result.IsFailed)
    {
        return result.FailureReason switch
        {
            EntityNotFoundFailure => this.NotFound(),
            _ => this.BadRequest()
        };
    }
    
    return this.Ok(result.Value);
}
```