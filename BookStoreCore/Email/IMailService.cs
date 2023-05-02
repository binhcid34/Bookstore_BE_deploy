using BookStoreCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreCore.Email
{
    public interface IMailService
    {
        bool SendMailAsync(string SendMailTo, string SendMailSubject, string SendMailBody);
    }
}
