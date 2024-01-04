namespace Application.common.Security;

/// <summary>
///   Specifies that a class should bypass authorization checks.
///   This attribute can be applied to classes and will override the authorization checks for
///   all methods within the class.
/// </summary>
[ AttributeUsage(AttributeTargets.Class, Inherited = false) ]
public sealed class BypassAuthorizationAttribute : Attribute
{
}
