using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;

namespace SocialsNetwork.Endpoints.DicionaryErrors
{
    public static class ProblemDetails
    {
        public static Dictionary<string, string[]> convertToDetails(this IReadOnlyCollection<Notification> notifications)
        {
           return notifications
                    .GroupBy(g => g.Key)
                    .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
        }

        public static Dictionary<string, string[]> convertToDetails(this IEnumerable<IdentityError> error)
        {
           // var dictionary = new Dictionary<string, string[]>();
           // dictionary.Add("Error", error.Select(e => e.Description).ToArray());
           // return dictionary;
            return error
                     .GroupBy(g => g.Code)
                     .ToDictionary(g => g.Key, g => g.Select(x => x.Description).ToArray());
        }
    }
}
