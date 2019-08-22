using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    /// <summary>
    /// 登陆用户基本信息
    /// </summary>
    public class UserInfo : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            //这里可以重写用户ID，重写后可以直接在Hub的派生类中（ChatHub）直接指定用户ID 推送消息
            //var requestContext = connection.GetHttpContext();
            //requestContext.Request.Session
            //requestContext.Request.QueryString["xxx"]
            //requestContext.Request.Cookies[""]
            var userId = Guid.NewGuid().ToString();
            //此处模拟一个用户ID，
            return userId;
        }
    }
}
