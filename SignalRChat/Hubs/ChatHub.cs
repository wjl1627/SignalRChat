using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub:Hub
    {
        ////这里简单记录用户名（假设唯一）与连接ID
        //private static Dictionary<string, string> AllUserDic = new Dictionary<string, string>();
        ///// <summary>
        ///// 发送消息
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="reUser"></param>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //public async Task SendMessage(string user, string reUser,string message)
        //{
        //    //与包含自己的所有客户端通讯（收到消息）
        //    //await Clients.All.SendAsync("ReceiveMessage", user, message);
        //    //与除自己之外的其它客户端通讯（收到消息）
        //    //await Clients.Others.SendAsync("ReceiveMessage", user, message);
        //    //仅与自己通讯（收消息）
        //    //await Clients.Caller.SendAsync("ReceiveMessage", user, message);
        //    if (AllUserDic.ContainsKey(user))
        //        AllUserDic[user] = Context.ConnectionId;
        //    else
        //        AllUserDic.Add(user, Context.ConnectionId);

        //    if (string.IsNullOrEmpty(reUser))
        //        await Clients.Others.SendAsync("ReceiveMessage", user, reUser, message);
        //    else if (AllUserDic.ContainsKey(reUser))
        //        await Clients.Clients(AllUserDic[reUser]).SendAsync("ReceiveMessage", user, reUser, message);
        //    else
        //        await Clients.Caller.SendAsync("ReceiveMessage", user,reUser, "消息发送失败");
        //}
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="user">消息发送人（这个只是用来展示 是谁发的而已）</param>
        /// <param name="reUserId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, string reUserId, string message)
        {
            //其实user不用传，可以从 Context.User.Claims 或 Context.GetHttpContext() 里面拿到user的所有信息
            //此时传入user 只是用来偷懒，因为demo没有做登陆之类的
            if (string.IsNullOrEmpty(reUserId))
                await Clients.Others.SendAsync("ReceiveMessage", user, reUserId, message);
            else
                await Clients.User(reUserId).SendAsync("ReceiveMessage", user, reUserId, message);
        }
        /// <summary>
        /// 获取当前用户的Id
        /// 这一步主要是为了方便前端页面打开的时候，能显示自己的ID
        /// </summary>
        /// <returns></returns>
        public async Task GetCurrentUserId()
        {
            var connectionId = Context.ConnectionId;//这个连接ID
            var currentUserId = Context.UserIdentifier;//这就是我们重写的GetUserId
            //给自己发消息
            await Clients.User(currentUserId).SendAsync("SenUserId", currentUserId);
        }
        /// <summary>
        /// 创建连接的时候会走这里
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {            
            return base.OnConnectedAsync();
        }
        /// <summary>
        /// 断开连接的时候会走这里
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
