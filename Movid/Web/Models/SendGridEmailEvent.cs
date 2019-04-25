using System;

namespace Movid.App.Models
{

//    [
//  {
//    "email": "john.doe@sendgrid.com",
//    "timestamp": 1337197600,
//    "smtp-id": "<4FB4041F.6080505@sendgrid.com>",
//    "event": "processed"
//  },
//  {
//    "email": "john.doe@sendgrid.com",
//    "timestamp": 1337966815,
//    "category": "newuser",
//    "event": "click",
//    "url": "http://sendgrid.com"
//  },
//  {
//    "email": "john.doe@sendgrid.com",
//    "timestamp": 1337969592,
//    "smtp-id": "<20120525181309.C1A9B40405B3@Example-Mac.local>",
//    "event": "processed"
//  }
//]


    public class SendGridEmailEvent
    {
        //email=emailrecipient@domain.com&event=open&userid=1123&template=welcome&category=user_signup

        public int Id { get; set; }
        public string Email { get; set; }
        public string @Event { get; set; }
        public string ProgramId { get; set; }
        public string Category { get; set; }
        public int Timestamp { get; set; }

    }
}