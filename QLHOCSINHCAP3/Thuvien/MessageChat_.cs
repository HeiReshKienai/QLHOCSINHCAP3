using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLHOCSINHCAP3.Thuvien
{
    internal class MessageChat_
    {

        //Id messsage
        public ObjectId Id { get; set; }
        //Id nguoi gui
        public ObjectId UserSend { get; set; }
        //Id nguoi nhan
        public ObjectId UserRecevie { get; set; }
        // thoi gian gui
        public DateTime send_at { get; set; }
        //Messsage chat
        public string Message { set; get; }


        public int index { set; get; }


        public MessageChat_(ObjectId user_send, ObjectId user_recive, string message, int index)
        {
            this.UserSend = user_send;
            this.UserRecevie = user_recive;
            this.Message = message;
            this.send_at = DateTime.Now;
            this.index = index;
        }
    }
}
