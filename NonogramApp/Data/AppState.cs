namespace NonogramApp.Data
{
    /// <summary>
    /// Holds application-wide state, such as the currently logged-in user.
    /// </summary>
    public static class AppState
    {
        /// <summary>
        /// The username of the currently logged-in user. Set this after a successful login.
        /// </summary>
        public static string? CurrentUsername { get; set; }

        /// <summary>
        /// Optionally, store the full User object for the current user.
        /// </summary>
        public static NonogramApp.Models.User? CurrentUser { get; set; }
    }
}