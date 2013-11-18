using System;
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

        public void OnNext(DocumentChangeNotification documentChangeNotification)
        {
            if (documentChangeNotification.Type == DocumentChangeTypes.Put)//new document inserted
            {
                Email newEmail;
                using (var session = MvcApplication.Store.OpenSession())
                {
                    newEmail = session.Load<Email>(documentChangeNotification.Id);//get document by id
                }

                new EmailHub().Send(newEmail.Sender, newEmail.Subject);//notify clients
            }
        }
    }
}