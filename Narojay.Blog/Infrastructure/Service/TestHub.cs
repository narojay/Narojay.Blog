using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Narojay.Blog.Infrastructure.Service
{
    public class TestHub : Hub
    {
        //重写连接事件，初次建立连接时进入此方法，开展具体业务可使用，例如管理连接池。
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("connected", Context.ConnectionId);
        }

        //重写断开事件，同理。
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        //服务端接收客户端发送方法
        public async Task SendMessage(string message)
        {
            //第一个参数为客户端接收服务端返回方法，名称需要服务端一致。
            await Clients.Caller.SendAsync("ReceiveMessage", Context.ConnectionId + ":  " + message);
        }

        public async Task SendAllMessage(string message)
        {
            //第一个参数为客户端接收服务端返回方法，名称需要服务端一致。
            await Clients.All.SendAsync("ReceiveMessage", Context.ConnectionId + ":  " + message);
        }
    }
}