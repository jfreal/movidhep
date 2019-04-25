using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movid.App.Infrastructure.Emails
{
    public enum EmailEnum
    {
        CompanyNewUserNotification,
        NewAccountOpen,
        SendInvitation,
        SendRandomizedProgramToLoggedInUser,
        SendResetEmail,
        SendToPatient
    }
}