namespace Application.common.interfaces;

public interface IApplicationUser
{
  Guid            Id                             { get; }
  string          UserName                       { get; set; }
  string          NormalizedUserName             { get; set; }
  string          Email                          { get; set; }
  string          NormalizedEmail                { get; set; }
  bool            EmailConfirmed                 { get; set; }
  string          PasswordHash                   { get; set; }
  string          SecurityStamp                  { get; set; }
  string          ConcurrencyStamp               { get; set; }
  string          PhoneNumber                    { get; set; }
  bool            PhoneNumberConfirmed           { get; set; }
  bool            TwoFactorEnabled               { get; set; }
  DateTimeOffset? LockoutEnd                     { get; set; }
  bool            LockoutEnabled                 { get; set; }
  int             AccessFailedCount              { get; set; }
  string          DisplayName                    { get; }
  string          Bio                            { get; }
  string?         RefreshToken                   { get; }
  DateTime        RefreshTokenExpirationDateTime { get; }
}
