using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using Raven.Abstractions.Data;
using WebUI.Hubs;

namespace WebUI.Models
{
    public class EmailsObserver : IObserver<DocumentChangeNotification>
    {
        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            //ToDo:handle exception
        }

        public void OnNext(DocumentChangeNotification value)
        {
            if (value.Type == DocumentChangeTypes.Put)
            {
                Email newEmail;
                using (var session = MvcApplication.Store.OpenSession())
                {
                    newEmail = session.Load<Email>(value.Id);
                }

                new EmailHub().Send(newEmail.Sender, newEmail.Subject);
            }
        }
    }
}