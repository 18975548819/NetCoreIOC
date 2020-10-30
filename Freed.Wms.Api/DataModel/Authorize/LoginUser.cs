using System;

namespace DataModel.Authorize
{
    public class LoginUser
    {
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public string GroupType { get; set; }
        public string WmsRepertory { get; set; }
    }
}
