using SafeVaultDemo.Models;

namespace SafeVault.Services;

public class AuthorizationService
{
    public bool CanAccessAdminDashboard(User user)
    {
        return user.Role.Equals(
            "Admin",
            StringComparison.OrdinalIgnoreCase);
    }

    public bool CanAccessUserArea(User user)
    {
        return user.Role.Equals(
                   "User",
                   StringComparison.OrdinalIgnoreCase)
               ||
               user.Role.Equals(
                   "Admin",
                   StringComparison.OrdinalIgnoreCase);
    }

    public bool HasRole(
        User user,
        string role)
    {
        return user.Role.Equals(
            role,
            StringComparison.OrdinalIgnoreCase);
    }
}