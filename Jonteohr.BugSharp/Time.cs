using BugSharp.Remote;

namespace BugSharp
{
    /// <summary>
    /// The main BugZilla Time object
    /// </summary>
    public class Time
    {
        /// <summary>
        /// Create a new Time object
        /// </summary>
        /// <param name="remoteTime">A <see cref="RemoteTime"/> object to inherit from</param>
        public Time(RemoteTime remoteTime)
        {
            DbTime = remoteTime.db_time;
            WebTime = remoteTime.web_time;
            WebTimeUtc = remoteTime.web_time_utc;
            TzName = remoteTime.tz_name;
            TzShortName = remoteTime.tz_short_name;
            TzOffset = remoteTime.tz_offset;
        }
        
        /// <summary>
        /// The current time in UTC, according to the Bugzilla database server.<br />
        /// Note that Bugzilla assumes that the database and the webserver are running in the same time zone. However, if the web server and the database server aren’t synchronized or some reason, this is the time that you should rely on or doing searches and other input to the WebService.
        /// </summary>
        public string DbTime { get; }
        
        /// <summary>
        /// This is the current time in UTC, according to Bugzilla’s web server.<br />
        /// This might be different by a second from <see cref="DbTime"/> since this comes from a different source. If it’s any more different than a second, then there is likely some problem with this Bugzilla instance. In this case you should rely on the <see cref="DbTime"/>, not the <see cref="WebTime"/>.
        /// </summary>
        public string WebTime { get; }
        
        /// <summary>
        /// Identical to <see cref="WebTime"/>. (Exists only for backwards-compatibility with versions of Bugzilla before 3.6.)
        /// </summary>
        public string WebTimeUtc { get; }
        
        /// <summary>
        /// The literal string UTC. (Exists only for backwards-compatibility with versions of Bugzilla before 3.6.)
        /// </summary>
        public string TzName { get; }
        
        /// <summary>
        /// The literal string UTC. (Exists only for backwards-compatibility with versions of Bugzilla before 3.6.)
        /// </summary>
        public string TzShortName { get; }
        
        /// <summary>
        /// The literal string +0000. (Exists only for backwards-compatibility with versions of Bugzilla before 3.6.)
        /// </summary>
        public string TzOffset { get; }
    }
}