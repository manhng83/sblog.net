#region Disclaimer/License Info

/* *********************************************** */

// sBlog.Net

// sBlog.Net is a minimalistic blog engine software.

// Homepage: http://sblogproject.net
// Github: http://github.com/karthik25/sBlog.Net

// This project is licensed under the BSD license.  
// See the License.txt file for more information.

/* *********************************************** */

#endregion
namespace sBlog.Net.Enumerations
{
    public enum PostStatus
    {
        all = 0,
        pub = 2,
        pri = 1
    }

    /// <summary>
    /// EntryType = 1 for Posts and EntryType = 2 for Pages
    /// </summary>
    public enum EntryTypeDef
    {
        /// <summary>
        /// EntryType = 1 for posts
        /// </summary>
        Posts = 1, // EntryType = 1 for posts
        /// <summary>
        /// EntryType = 2 for pages
        /// </summary>
        Pages = 2 // EntryType = 2 for pages
    }

}
