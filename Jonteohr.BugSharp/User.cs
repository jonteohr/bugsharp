namespace BugSharp
{
    /// <summary>
    /// A struct containing user session information
    /// </summary>
    public struct User
    {
        /// <summary>
        /// Numeric ID of the user that was logged in.
        /// </summary>
        public int Id { get; }
        /// <summary>
        /// The username of the user
        /// </summary>
        public string Username { get; }
        /// <summary>
        /// Token which can be passed in the parameters as authentication in other calls. The token can be sent along with any future requests to the webservice, for the duration of the session, i.e. til <see cref="IUserService.Logout"/> is called.
        /// </summary>
        public string Token { get; }

        internal User(int id, string username, string token)
        {
            Id = id;
            Username = username;
            Token = token;
        }
    }
}