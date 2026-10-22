using Authentication.Domain.ExternalLogin.Google;
using Authentication.Shared.ExternalLogin.UserProfile;
using Authentication.Shared.ExternalLogin.UserProfile.Readers;
using Google.Apis.Auth.OAuth2;
using Google.Apis.PeopleService.v1;
using Google.Apis.Services;
using Microsoft.Extensions.Logging;

namespace Authentication.Application.ExternalLogin.UserProfile.Readers;

public class GoogleUserProfileGetter : IUserProfileGetter
{
    private readonly ILogger<ExternalUserInfoGetterFactory> _logger;

    public GoogleUserProfileGetter(ILogger<ExternalUserInfoGetterFactory> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IExternalUserProfile?> GetProfileAsync(string authenticationProvider, string? accessToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            _logger.LogWarning("Google external login did not contain an access token.");
            return null;
        }

        var credential = GoogleCredential.FromAccessToken(accessToken);
        var peopleService = new PeopleServiceService(
            new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "ISSK"
            });

        var personRequest = peopleService.People.Get("people/me");
        personRequest.PersonFields = "birthdays,names,phoneNumbers,emailAddresses";
        var person = await personRequest.ExecuteAsync(CancellationToken.None);

        var name = person.Names?.FirstOrDefault();
        var phoneNumber = person.PhoneNumbers?.FirstOrDefault()?.Value;
        var email = person.EmailAddresses?.FirstOrDefault()?.Value;
        var date = person.Birthdays?
            .Select(birthday => birthday.Date)
            .FirstOrDefault(birthday =>
                birthday is not null &&
                birthday.Year > 0 &&
                birthday.Month is >= 1 and <= 12 &&
                birthday.Day is >= 1 and <= 31);

        return new ExternalUserProfile()
        {
            FirstName = name?.GivenName,
            LastName = name?.FamilyName,
            PhoneNumber = phoneNumber,
            Email = email,
            BirthDate = date is null
                ? null
                : new DateOnly(
                    date.Year.GetValueOrDefault(),
                    date.Month.GetValueOrDefault(),
                    date.Day.GetValueOrDefault())
        };
    }
}