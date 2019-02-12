namespace QrF.Core.ComFr.Constant
{
    public enum RecordStatus
    {
        Active = 1,
        InActive = 2
    }
    public enum ActionType
    {
        Create = 1,
        Update = 2,
        Delete = 3,
        Design = 4,
        Publish = 5,
        Unattached = 6,
        Continue = 7,
        UnAttach = 8
    }
    public enum MediaType
    {
        Folder = 1,
        Image = 2,
        Video = 3,
        Zip = 4,
        Pdf = 5,
        Txt = 6,
        Doc = 7,
        Excel = 8,
        Ppt = 9,
        Other = 100
    }
    public enum UserType
    {
        Administrator = 1,
        Customer = 2
    }
    public enum DbTypes
    {
        /// <summary>
        /// MsSql 2012 and later
        /// </summary>
        MsSql = 1,
        /// <summary>
        /// Before MsSql 2012
        /// </summary>
        MsSqlEarly = 2,
        /// <summary>
        /// SQLlite
        /// </summary>
        Sqlite = 3,
        /// <summary>
        /// MySql
        /// </summary>
        MySql = 4,
        /// <summary>
        /// Oracle
        /// </summary>
        Oracle = 5
    }
}
