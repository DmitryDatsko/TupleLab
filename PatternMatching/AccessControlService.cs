namespace TupleLab.PatternMatching;

public class AccessControlService
{
    public enum Role
    {
        Guest,
        User,
        Moderator,
        Admin
    }

    public enum Resource
    {
        PublicData,
        UserData,
        ModeratorPanel,
        AdminPanel
    }

    public static (bool HasAccess, string Reason, int StatusCode) CheckAccess(
        Role userRole,
        Resource resource,
        bool isOwner
    ) { 
        return (userRole, resource, isOwner) switch{
            (Role.Admin, _, _) => (true, $"You're {userRole}", 200),
            (Role.Moderator, Resource.PublicData or Resource.UserData or Resource.ModeratorPanel, _) => (true, $"You're {userRole}", 200),
            (Role.User, Resource.PublicData or Resource.UserData, true) => (true, $"You're {userRole}", 200),
            (Role.Guest, Resource.PublicData, _) => (true, $"You're {userRole}", 200),
            _ => (false, $"You're {userRole}", 403)
        };
    }
}
